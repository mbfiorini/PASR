using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Castle.Facilities.Logging;
using Abp.AspNetCore;
using Abp.AspNetCore.Mvc.Antiforgery;
using Abp.Castle.Logging.Log4Net;
using PASR.Authentication.JwtBearer;
using PASR.Configuration;
using PASR.Identity;
using PASR.Web.Resources;
using Abp.AspNetCore.SignalR.Hubs;
using Abp.Dependency;
using Abp.Json;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;


namespace PASR.Web.Startup
{
    public class Startup
    {
        private readonly IConfigurationRoot _appConfiguration;

        public Startup(IWebHostEnvironment env)
        {
            //Método de extensão no projeto Web.Core, Localiza o arquivo appSettings.json no diretório Raiz
            _appConfiguration = env.GetAppConfiguration();
        }

        //Configura os serviços (Adiciona classes no Container de DI)
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //MVC
            services.AddControllersWithViews(
                    options =>
                    {
                        //Adiciona o Filtro de AntiForgery em todas as actions dos controllers, não apenas nos posts
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                        options.Filters.Add(new AbpAutoValidateAntiforgeryTokenAttribute());
                    }
                )
                .AddRazorRuntimeCompilation() //Adiciona compilação dos arquivos .cshtml em tempo de execução (Caso tenham alterado a versão)
                .AddNewtonsoftJson(options =>
                {
                    //Adiciona serialização de Objetos .NET para JSON personalizada no ABP. Recebe a instância da classe de DI do Casle Windsor
                    options.SerializerSettings.ContractResolver = new AbpMvcContractResolver(IocManager.Instance)
                    {
                        NamingStrategy = new CamelCaseNamingStrategy() //CamelCase para serializar e deserializar os objetos
                    };
                });

            //Registra todas as classes para controle de autenticação no Container de DI (User, Roles, Tenants, etc...)
            IdentityRegistrar.Register(services);

            //Adiciona todos os serviços middlewares de autenticação no Container de DI
            AuthConfigurer.Configure(services, _appConfiguration);

            //Adiciona uma classe que armazenará os dados dos .js na pasta www.root e pode ser usada para escrever nas respostas para o client
            services.AddScoped<IWebResourceManager, WebResourceManager>();

            services.AddSignalR();

            // Configure Abp and Dependency Injection
            return services.AddAbp<PASRWebMvcModule>(
                // Configure Log4Net logging, with the log4net configuration File
                options => options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                    f => f.UseAbpLog4Net().WithConfig("log4net.config")
                )
            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseAbp(); //Initializes ABP framework.

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            //Adiciona um middleware para verificar se o client já está autenticado, se não estiver, 
            //tenta logá-lo com o Token recebido pelo Header do client e no final chama o próximo Middleware no Pipeline
            app.UseJwtTokenMiddleware();

            //Adiciona o mecanismo padrão de autorização do ASP.NET Core
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<AbpCommonHub>("/signalr");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("defaultWithArea", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

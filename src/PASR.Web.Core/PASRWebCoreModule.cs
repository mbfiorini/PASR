using System;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.AspNetCore.SignalR;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.Configuration;
using PASR.Authentication.JwtBearer;
using PASR.Configuration;
using PASR.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace PASR
{
    [DependsOn(
         typeof(PASRApplicationModule),
         typeof(PASREntityFrameworkModule),
         typeof(AbpAspNetCoreModule)
        ,typeof(AbpAspNetCoreSignalRModule)
     )]
    public class PASRWebCoreModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public PASRWebCoreModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            //Armazena nas configurações do módulo a conectionString do appSettings.json localizado no diretório Raiz do módulo
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                PASRConsts.ConnectionStringName
            );

            // Use database for language management
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            //Configura todos os ApplicationServices para serem controllers (com EndPoints para as requisições)
            Configuration.Modules.AbpAspNetCore()
                 .CreateControllersForAppServices(
                     typeof(PASRApplicationModule).GetAssembly()
                 );

            //Configura o Middleware que verifica se o usuário está autenticado através do token que ele enviará em cada requisição
            ConfigureTokenAuth();
        }

        private void ConfigureTokenAuth()
        {
            /*Registra o configurador de Token de Autenticação no container de injeção de dependência como Singleton e já o instancia no ato do registro.
            isso indica que essa instância criada por este método será utilizada até o Host ser derrubado*/
            IocManager.Register<TokenAuthConfiguration>(); 

            var tokenAuthConfig = IocManager.Resolve<TokenAuthConfiguration>();

            tokenAuthConfig.SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appConfiguration["Authentication:JwtBearer:SecurityKey"]));
            tokenAuthConfig.Issuer = _appConfiguration["Authentication:JwtBearer:Issuer"];
            tokenAuthConfig.Audience = _appConfiguration["Authentication:JwtBearer:Audience"];
            tokenAuthConfig.SigningCredentials = new SigningCredentials(tokenAuthConfig.SecurityKey, SecurityAlgorithms.HmacSha256);
            tokenAuthConfig.Expiration = TimeSpan.FromDays(1); //O token de cada sessão expira em 1 dia
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PASRWebCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(PASRWebCoreModule).Assembly);
        }
    }
}

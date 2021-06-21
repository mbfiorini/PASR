using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using PASR.Configuration;
using PASR.EntityFrameworkCore;

namespace PASR.Web.Startup
{
    [DependsOn(typeof(PASRWebCoreModule))]
    public class PASRWebMvcModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public PASRWebMvcModule(IWebHostEnvironment env, PASREntityFrameworkModule entityFrameworkModule)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
            //WebCoreModule depends on EFCore Module, thus we can inject EFCore Module into this one and decide if DBSeed Actually is going to happen
            entityFrameworkModule.SkipDbSeed = _appConfiguration.GetSection("App").GetValue<bool>("skipDbSeed");
        }

        public override void PreInitialize()
        {
            //Adiciona na lista de classes de navegação a classe que configura os itens que serão exibidos no menu de navegação, ele será injetado nas Views
            Configuration.Navigation.Providers.Add<PASRNavigationProvider>();
        }

        public override void Initialize()
        {
            /*Injeta os serviços no Container do Castle Windsor, olhando para as classes que implmentam as interfaces indicando que devem ser injetadas
            como o ITransientDependecy no módulo de aplicação */
            IocManager.RegisterAssemblyByConvention(typeof(PASRWebMvcModule).GetAssembly());
        }
    }
}

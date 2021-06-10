using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using PASR.Authorization;

namespace PASR
{
    [DependsOn(
        typeof(PASRCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class PASRApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<PASRAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(PASRApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}

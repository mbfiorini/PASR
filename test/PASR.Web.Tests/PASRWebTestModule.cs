using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using PASR.EntityFrameworkCore;
using PASR.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace PASR.Web.Tests
{
    [DependsOn(
        typeof(PASRWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class PASRWebTestModule : AbpModule
    {
        public PASRWebTestModule(PASREntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PASRWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(PASRWebMvcModule).Assembly);
        }
    }
}
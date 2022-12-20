using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using HotSpotWeb.EntityFrameworkCore;
using HotSpotWeb.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace HotSpotWeb.Web.Tests
{
    [DependsOn(
        typeof(HotSpotWebWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class HotSpotWebWebTestModule : AbpModule
    {
        public HotSpotWebWebTestModule(HotSpotWebEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(HotSpotWebWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(HotSpotWebWebMvcModule).Assembly);
        }
    }
}
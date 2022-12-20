using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using HotSpotWeb.Authorization;

namespace HotSpotWeb
{
    [DependsOn(
        typeof(HotSpotWebCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class HotSpotWebApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<HotSpotWebAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(HotSpotWebApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}

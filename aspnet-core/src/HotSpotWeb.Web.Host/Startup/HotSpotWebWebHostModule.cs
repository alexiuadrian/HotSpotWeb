using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using HotSpotWeb.Configuration;

namespace HotSpotWeb.Web.Host.Startup
{
    [DependsOn(
       typeof(HotSpotWebWebCoreModule))]
    public class HotSpotWebWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public HotSpotWebWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(HotSpotWebWebHostModule).GetAssembly());
        }
    }
}

using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace HotSpotWeb.Controllers
{
    public abstract class HotSpotWebControllerBase: AbpController
    {
        protected HotSpotWebControllerBase()
        {
            LocalizationSourceName = HotSpotWebConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}

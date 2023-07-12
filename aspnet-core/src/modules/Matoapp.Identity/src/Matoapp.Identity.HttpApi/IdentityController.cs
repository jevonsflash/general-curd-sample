using Matoapp.Identity.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Matoapp.Identity
{
    public abstract class IdentityController : AbpControllerBase
    {
        protected IdentityController()
        {
            LocalizationResource = typeof(IdentityResource);
        }
    }
}
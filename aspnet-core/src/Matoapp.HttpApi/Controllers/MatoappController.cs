using Matoapp.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Matoapp.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class MatoappController : AbpControllerBase
{
    protected MatoappController()
    {
        LocalizationResource = typeof(MatoappResource);
    }
}

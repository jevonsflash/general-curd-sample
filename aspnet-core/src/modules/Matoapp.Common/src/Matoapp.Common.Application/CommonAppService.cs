using Matoapp.Common.Localization;
using Volo.Abp.Application.Services;

namespace Matoapp.Common;

public abstract class CommonAppService : ApplicationService
{
    protected CommonAppService()
    {
        LocalizationResource = typeof(CommonResource);
        ObjectMapperContext = typeof(CommonApplicationModule);
    }
}

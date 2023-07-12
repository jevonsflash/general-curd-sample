using Matoapp.Health.Localization;
using Volo.Abp.Application.Services;

namespace Matoapp.Health;

public abstract class HealthAppService : ApplicationService
{
    protected HealthAppService()
    {
        LocalizationResource = typeof(HealthResource);
        ObjectMapperContext = typeof(HealthApplicationModule);
    }
}

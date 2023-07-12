using Localization.Resources.AbpUi;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Matoapp.Health.Localization;

namespace Matoapp.Health;

[DependsOn(
    typeof(HealthApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class HealthHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(HealthHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<HealthResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}

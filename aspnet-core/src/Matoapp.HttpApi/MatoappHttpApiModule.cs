using Localization.Resources.AbpUi;
using Matoapp.Common;
using Matoapp.Health;
using Matoapp.Identity;
using Matoapp.Localization;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace Matoapp;

[DependsOn(
    typeof(MatoappApplicationContractsModule),
    typeof(AbpAccountHttpApiModule),
    typeof(AbpIdentityHttpApiModule),
    typeof(AbpPermissionManagementHttpApiModule),
    typeof(AbpTenantManagementHttpApiModule),
    typeof(AbpFeatureManagementHttpApiModule),
    typeof(AbpSettingManagementHttpApiModule),
    typeof(CommonHttpApiModule),
    typeof(HealthHttpApiModule),
    typeof(IdentityHttpApiModule)
    )]
public class MatoappHttpApiModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureLocalization();
    }

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<MatoappResource>()
                .AddBaseTypes(
                    typeof(AbpUiResource)
                );
        });
    }
}

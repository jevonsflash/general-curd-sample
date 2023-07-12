using Matoapp.Common;
using Matoapp.Health;
using Matoapp.Identity;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace Matoapp;

[DependsOn(
    typeof(MatoappDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(MatoappApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule),

    typeof(CommonApplicationModule),
    typeof(HealthApplicationModule),
    typeof(IdentityApplicationModule)
    )]
public class MatoappApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<MatoappApplicationModule>();
        });
    }
}

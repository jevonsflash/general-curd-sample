using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Matoapp.Health;

[DependsOn(
    typeof(HealthDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class HealthApplicationContractsModule : AbpModule
{

}

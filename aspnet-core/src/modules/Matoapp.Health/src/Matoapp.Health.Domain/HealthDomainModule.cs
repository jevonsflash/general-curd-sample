using Matoapp.Identity;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Matoapp.Health;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(IdentityHttpApiClientModule),
    typeof(HealthDomainSharedModule)
)]
public class HealthDomainModule : AbpModule
{

}

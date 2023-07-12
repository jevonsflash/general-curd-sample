using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Matoapp.Common;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(CommonDomainSharedModule)
)]
public class CommonDomainModule : AbpModule
{

}

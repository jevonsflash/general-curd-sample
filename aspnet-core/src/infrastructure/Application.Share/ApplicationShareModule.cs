using Domain.Share;
using Volo.Abp.Modularity;

namespace Application.Share
{
    [DependsOn(
    typeof(DomainShareModule))]
    public class ApplicationShareModule : AbpModule
    {
    }
}

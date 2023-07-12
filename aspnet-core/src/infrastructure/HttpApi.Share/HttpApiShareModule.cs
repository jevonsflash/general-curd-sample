using Application.Share;
using Volo.Abp.Modularity;

namespace HttpApi.Share
{
    [DependsOn(
typeof(ApplicationShareModule))]
    public class HttpApiShareModule : AbpModule
    {
    }
}

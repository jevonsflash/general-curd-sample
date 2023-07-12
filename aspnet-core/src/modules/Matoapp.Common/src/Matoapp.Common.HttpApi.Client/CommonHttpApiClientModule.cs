using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Matoapp.Common;

[DependsOn(
    typeof(CommonApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class CommonHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(CommonApplicationContractsModule).Assembly,
            CommonRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CommonHttpApiClientModule>();
        });

    }
}

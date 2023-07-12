using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Matoapp.Health;

[DependsOn(
    typeof(HealthApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class HealthHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(HealthApplicationContractsModule).Assembly,
            HealthRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<HealthHttpApiClientModule>();
        });

    }
}

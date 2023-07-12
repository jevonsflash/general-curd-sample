using Application.Share;
using Domain.Share;
using Host.Share.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Host.Share
{
    [DependsOn(
  typeof(ApplicationShareModule))]
    public class HostShareModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            Configure<MvcOptions>(mvcOptions =>
            {
                mvcOptions.AddAbpResultFilter();
                mvcOptions.AddApiExplorer(context.Services);
            });
        }
    }
}

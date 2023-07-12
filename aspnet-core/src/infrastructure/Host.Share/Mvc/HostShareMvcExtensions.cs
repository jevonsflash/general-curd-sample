using Host.Share.Results;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Host.Share.Mvc;

public static class HostShareMvcExtensions
{

    public static void AddApiExplorer(this MvcOptions options, IServiceCollection services)
    {
#if DEBUG
        //options.Conventions.Add(new Fonlow.CodeDom.Web.ApiExplorerVisibilityEnabledConvention());
#endif

    }

    public static void AddAbpResultFilter(this MvcOptions options)
    {
        options.Filters.Add<AbpResultFilter>();

    }


}

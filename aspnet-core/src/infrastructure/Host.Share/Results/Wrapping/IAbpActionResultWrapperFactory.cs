using Microsoft.AspNetCore.Mvc.Filters;
using Volo.Abp.DependencyInjection;

namespace Host.Share.Results.Wrapping
{
    public interface IAbpActionResultWrapperFactory : ITransientDependency
    {
        IAbpActionResultWrapper CreateFor(FilterContext context);
    }
}
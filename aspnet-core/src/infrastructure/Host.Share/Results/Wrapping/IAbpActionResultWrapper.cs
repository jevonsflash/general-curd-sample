using Microsoft.AspNetCore.Mvc.Filters;

namespace Host.Share.Results.Wrapping
{
    public interface IAbpActionResultWrapper
    {
        void Wrap(FilterContext context);
    }
}
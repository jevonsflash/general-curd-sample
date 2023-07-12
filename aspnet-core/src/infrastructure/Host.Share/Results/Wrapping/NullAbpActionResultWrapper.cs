using Microsoft.AspNetCore.Mvc.Filters;

namespace Host.Share.Results.Wrapping
{
    public class NullAbpActionResultWrapper : IAbpActionResultWrapper
    {
        public void Wrap(FilterContext context)
        {

        }

    }
}
using System;

namespace Domain.Share.Web.Models
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method)]
    public class WrapResultAttribute : Attribute
    {

        public WrapResultAttribute()
        {
        }
    }
}

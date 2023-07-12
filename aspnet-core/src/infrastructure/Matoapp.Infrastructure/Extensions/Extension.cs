using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;
using Matoapp.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Matoapp.Infrastructure.Extensions
{
    public static class Extension
    {
        public static T GetRandom<T>(this IList<T> a)
        {
            return CommonHelper.GetRandom(a);
        }
    }
}

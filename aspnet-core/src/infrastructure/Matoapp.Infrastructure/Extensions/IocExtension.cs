using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Matoapp.Infrastructure.Extensions
{
    public static class IocExtension
    {
        public static IDisposable Begin(this IWindsorContainer container)
        {
            return container.BeginScope();
        }
    }
}

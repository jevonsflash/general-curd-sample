using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matoapp.Infrastructure.Extensions
{
    public static class EnumerableExtension
    {
        public static IEnumerable<T> PageBy<T>(this IEnumerable<T> query, int skipCount, int maxResultCount)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            return query.Skip(skipCount).Take(maxResultCount);
        }

        public static List<T> PageBy<T>(this IList<T> query, int skipCount, int maxResultCount)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            return query.Skip(skipCount).Take(maxResultCount).ToList();
        }
    }
}

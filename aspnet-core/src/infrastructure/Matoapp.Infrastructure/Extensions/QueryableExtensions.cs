using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Matoapp.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> WhereIfElse<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate, Expression<Func<T, bool>> elsePredicate)
        {
            return condition
                ? query.Where(predicate)
                : query.Where(elsePredicate);
        }
    }
}

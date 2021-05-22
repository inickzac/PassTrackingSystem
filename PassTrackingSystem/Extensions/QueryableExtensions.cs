using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PassTrackingSystem.Extensions
{
    public static partial class QueryableExtensions
    {
        public static IQueryable<T> OrderByMember<T>(this IQueryable<T> source, string memberPath, bool descending)
        {
            var parameter = Expression.Parameter(typeof(T), "item");
            var member = memberPath.Split('.')
                .Aggregate((Expression)parameter, Expression.PropertyOrField);
            var keySelector = Expression.Lambda(member, parameter);
            var methodCall = Expression.Call(
                typeof(Queryable), descending ? "OrderByDescending" : "OrderBy",
                new[] { parameter.Type, member.Type },
                source.Expression, Expression.Quote(keySelector));
            return (IQueryable<T>)source.Provider.CreateQuery(methodCall);
        }

        public static IQueryable<T> SearchByMember<T>(this IQueryable<T> query, string propertyName,
                 string searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm) && !string.IsNullOrEmpty(propertyName))
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var source = propertyName.Split('.').Aggregate((Expression)parameter,
                    Expression.Property);
                if (source.Type.Equals(typeof(string)))
                {
                    var body = Expression.Call(source, "Contains", Type.EmptyTypes,
                           Expression.Constant(searchTerm, typeof(string)));
                    return query.Where(Expression.Lambda<Func<T, bool>>(body, parameter));
                }
                if (source.Type.Equals(typeof(int)))
                {
                    var body = Expression.Equal(source, Expression.Constant(int.Parse(searchTerm)));
                    return query.Where(Expression.Lambda<Func<T, bool>>(body, parameter));
                }
                throw new InvalidOperationException("type not supported");
            }
            else return query;
        }
    }
}

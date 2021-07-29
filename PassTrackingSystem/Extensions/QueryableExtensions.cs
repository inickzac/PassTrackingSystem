using PassTrackingSystem.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
                    return StringExpression(query, searchTerm, parameter, source);
                }
                if (source.Type.Equals(typeof(int)))
                {
                    return IntExpression(query, searchTerm, parameter, source);
                }
                if (source.Type.Equals(typeof(DateTime)))
                {
                    return DateTimeExpression(query, searchTerm, parameter, source, propertyName);
                }
                if (source.Type.Equals(typeof(Employee)))
                {
                    return EmployeeExpression(query, searchTerm, parameter, source);
                }              
                throw new InvalidOperationException("type not supported");
            }
            else return query;
        }

        private static IQueryable<T> StringExpression<T>(IQueryable<T> query, string searchTerm, ParameterExpression parameter, Expression source)
        {
            var body = Expression.Call(source, "Contains", Type.EmptyTypes,
                   Expression.Constant(searchTerm, typeof(string)));
            return query.Where(Expression.Lambda<Func<T, bool>>(body, parameter));
        }

        private static IQueryable<T> IntExpression<T>(IQueryable<T> query, string searchTerm, ParameterExpression parameter, Expression source)
        {
            var body = Expression.Equal(source, Expression.Constant(int.Parse(searchTerm)));
            return query.Where(Expression.Lambda<Func<T, bool>>(body, parameter));
        }

        private static IQueryable<T> DateTimeExpression<T>(IQueryable<T> query, string searchTerm,
            ParameterExpression parameter, Expression source, string propertyName)
        {
            DateTime queryDate;
            if (DateTime.TryParse(searchTerm, out queryDate))
            {
                if (propertyName == "ValidWith")
                {
                    var body = Expression.GreaterThan(source, Expression.Constant(queryDate));
                    return query.Where(Expression.Lambda<Func<T, bool>>(body, parameter));
                }
                if (propertyName == "ValidUntil")
                {
                    var body = Expression.LessThan(source, Expression.Constant(queryDate));
                    return query.Where(Expression.Lambda<Func<T, bool>>(body, parameter));
                }

                var otherBody = Expression.Equal(source, Expression.Constant(queryDate));
                return query.Where(Expression.Lambda<Func<T, bool>>(otherBody, parameter));
            }
            else return query;
        }

        private static IQueryable<T> EmployeeExpression<T>(IQueryable<T> query, string searchTerm, ParameterExpression parameter, Expression source)
        {
            return query.Where(GetEqualOptionsFunc("Name"))
                .Concat(query.Where(GetEqualOptionsFunc("LastName")))
                .Concat(query.Where(GetEqualOptionsFunc("Patronymic")));

            Expression<Func<T, bool>> GetEqualOptionsFunc(string property)
            {
                var propertyEx = Expression.Property(source, typeof(Employee).GetProperty(property));
                var methodEx = Expression.Call(propertyEx, "Contains", Type.EmptyTypes,
                 Expression.Constant(searchTerm, typeof(string)));
                return Expression.Lambda<Func<T, bool>>(methodEx, parameter);
            }
        }     
    }
}

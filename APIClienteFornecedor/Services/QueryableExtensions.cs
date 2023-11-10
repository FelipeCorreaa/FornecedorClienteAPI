namespace APIClienteFornecedor.Services
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public static class QueryableExtensions
    {
        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string propertyName, bool descending = false)
        {
            var entityType = typeof(T);
            var propertyInfo = entityType.GetProperty(propertyName);

            if (propertyInfo == null)
            {
                throw new ArgumentException($"Property {propertyName} not found on type {entityType.Name}");
            }

            var parameter = Expression.Parameter(entityType, "x");
            var propertyAccess = Expression.MakeMemberAccess(parameter, propertyInfo);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);

            var methodName = descending ? "OrderByDescending" : "OrderBy";
            var resultExp = Expression.Call(typeof(Queryable), methodName,
                new Type[] { entityType, propertyInfo.PropertyType },
                query.Expression, Expression.Quote(orderByExp));

            return query.Provider.CreateQuery<T>(resultExp);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Talabat.core.Entities;
using Talabat.core.Specifications;

namespace Talabat.Repository.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;//_context.Set<TEntity>() [ Such...Product]

            if (spec != null) //p => p.Id == id
            {
                if (spec.Criteria != null)
                {
                    query = query.Where(spec.Criteria);

                }
                if (spec.OrderBy != null)
                {
                    query = query.OrderBy(spec.OrderBy);
                }
                if (spec.OrderByDescending != null)
                {
                    query = query.OrderByDescending(spec.OrderByDescending);
                }

                if (spec.Includes != null)
                {
                    query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
                }
            }
            return query;
        }
    }
}

using Azure.Core.Pipeline;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;
using Talabat.core.Specifications;

namespace Talabat.Repository.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> Spec)
        {
            var Query = inputQuery;//_context.Set<TEntity>() [ Such...Product]

            if (Spec is not null) //p => p.Id == id
            {
                Query = Query.Where(Spec.Criteria);
            }

            Query = Spec.Includes.Aggregate(Query, (currentQuery,includeExpression)=>currentQuery.Include(includeExpression));


            return Query;
        }
    }
}

using System.Linq.Expressions;
using Talabat.core.Entities;

namespace Talabat.core.Specifications
{
    public interface ISpecification<T> where T:BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression <Func<T,object>>> Includes { get; set; }
        
    }
}

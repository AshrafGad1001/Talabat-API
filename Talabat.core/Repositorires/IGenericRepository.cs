using Talabat.core.Entities;
using Talabat.core.Specifications;

namespace Talabat.core.Repositorires
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> Spec);
        Task<T> GetByIdWithSpecAsync(ISpecification<T> Spec);
        Task<int> GetCountAsync(ISpecification<T> spec);


        Task CreateAsync(T entity);

        void Update(T entity);
        void Delete(T entity);
    }
}

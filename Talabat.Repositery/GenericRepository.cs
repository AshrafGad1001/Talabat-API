using Microsoft.EntityFrameworkCore;
using Talabat.core.Entities;
using Talabat.core.Repositorires;
using Talabat.core.Specifications;
using Talabat.Repositery.Data;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        public GenericRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Product))
            {
                return (IEnumerable<T>)await _context.Set<Product>().Include(p => p.ProductBrand)
                                                    .Include(P => P.ProductType)
                                                    .ToListAsync();
            }
            return await _context.Set<T>().ToListAsync();
        }
        public Task<T> GetByIdAsync(int id)
        {
            return _context.Set<T>().Where(item => item.Id == id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> Spec)
        {
            return await ApplySpecification(Spec).ToListAsync();
        }
        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> Spec)
        {
            return await ApplySpecification(Spec).FirstOrDefaultAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> Spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), Spec);
        }

        public async Task<int> GetCountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }
    }
}

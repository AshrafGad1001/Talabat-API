using Microsoft.EntityFrameworkCore;
using Talabat.core.Entities;
using Talabat.core.Repositorires;
using Talabat.Repositery.Data;

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
                return (IEnumerable<T>) await _context.Set<Product>().Include(p=>p.ProductBrand)
                                                    .Include(P => P.ProductType)
                                                    .ToListAsync();
            }
            return await _context.Set<T>().ToListAsync();
        }

        public Task<T> GetByIdAsync(int id)
        {
            return _context.Set<T>().Where(item => item.Id == id).FirstOrDefaultAsync() ;
        }
    }
}

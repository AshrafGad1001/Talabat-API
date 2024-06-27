using System.Collections;
using Talabat.core.Entities;
using Talabat.core.Repositorires;
using Talabat.Repositery.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private Hashtable _Repositories;
        public UnitOfWork(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync(); 
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_Repositories == null)
            {
                _Repositories = new Hashtable();
            }
            var Type = typeof(TEntity).Name;

            if (!_Repositories.ContainsKey(Type))
            {
                var repository = new GenericRepository<TEntity>(_context);
                _Repositories.Add(Type, repository);
            }

            return (IGenericRepository<TEntity>)_Repositories[Type];
        }
    }
}

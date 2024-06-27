using Talabat.core.Entities;
using Talabat.core.Entities.OrderAggregate;

namespace Talabat.core.Repositorires
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

        Task<int> Complete();




        //IGenericRepository<Product> ProductsRepo();
        //IGenericRepository<ProductBrand> ProductBrandsRepo();
        //IGenericRepository<ProductType> ProductTypesRepo();
        //IGenericRepository<Order> OrdersRepo();
        //IGenericRepository<OrderItem> OrderItemsRepo();
        //IGenericRepository<DeliveryMethod> DeliveryMethodsRepo();
    }
}

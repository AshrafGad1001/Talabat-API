using Talabat.core.Entities;

namespace Talabat.core.Repositorires
{
    public interface ICartRepository
    {
        Task<CustomerCart> GetCartAsync(string cartId);
        Task<CustomerCart> UpdateCartAsync(CustomerCart cart);
        Task<bool> DeleteCart(string cartId);
    }
}

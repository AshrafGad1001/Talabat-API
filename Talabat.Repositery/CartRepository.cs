using StackExchange.Redis;
using System.Text.Json;
using Talabat.core.Entities;
using Talabat.core.Repositorires;

namespace Talabat.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly IDatabase _Database;
        public CartRepository(IConnectionMultiplexer redis)
        {
            _Database = redis.GetDatabase();
        }



        public async Task<bool> DeleteCart(string cartId)
        {
            return await _Database.KeyDeleteAsync(cartId);
        }

        public async Task<CustomerCart> GetCartAsync(string cartId)
        {
            var cart = await _Database.StringGetAsync(cartId);
            return cart.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerCart>(cart);
        }

        public async Task<CustomerCart> UpdateCartAsync(CustomerCart cart)
        {
            var CreatedOrUpdated = await _Database.StringSetAsync(cart.Id, JsonSerializer.Serialize(cart), TimeSpan.FromDays(30));
            if (CreatedOrUpdated == false)
            {
                return null;
            }
            return await GetCartAsync(cart.Id);
        }
    }
}

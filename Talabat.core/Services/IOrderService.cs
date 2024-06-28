using Talabat.core.Entities.OrderAggregate;

namespace Talabat.core.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string customerEmail, string cartId,
            int deliveryMethod, Address shippingAddress);

        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string customerEmail);

        Task<Order> GetOrderByIdForUserAsync(int orderId, string customerEmail);

        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}

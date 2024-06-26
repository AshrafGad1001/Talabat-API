﻿using Talabat.core.Entities.OrderAggregate;

namespace Talabat.core.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string customerEmail, string cartId,
            int deliveryMethod, Address shippingAddress);

        Task<IReadOnlyList<Order>> GetOrderForUserAsync(string customerEmail);

        Task<Order> GetOrderByIdForUserAsync(string orderId, string customerEmail);

        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}
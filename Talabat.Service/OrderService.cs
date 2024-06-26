using Talabat.core.Entities;
using Talabat.core.Entities.OrderAggregate;
using Talabat.core.Repositorires;
using Talabat.core.Services;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<DeliveryMethod> _deliveryMethodsRepo;
        private readonly IGenericRepository<Order> _ordersRepo;

        public OrderService(ICartRepository cartRepository
            , IGenericRepository<Product> productRepo,
            IGenericRepository<DeliveryMethod> DeliveryMethodsRepo,
            IGenericRepository<Order> OrdersRepo)
        {
            this._cartRepository = cartRepository;
            this._productRepo = productRepo;
            this._deliveryMethodsRepo = DeliveryMethodsRepo;
            this._ordersRepo = OrdersRepo;
        }

        public async Task<Order> CreateOrderAsync(string customerEmail, string cartId, int deliveryMethod, Address shippingAddress)
        {
            var cart = await _cartRepository.GetCartAsync(cartId);

            var orderItems = new List<OrderItem>();
            foreach (var item in cart.Items)
            {
                var product = await _productRepo.GetByIdAsync(item.Id);

                //To Ensure Information From DB
                var productitemordered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
                var orderitem = new OrderItem(productitemordered, product.Price, item.Quantity);

                ///Add To list
                orderItems.Add(orderitem);
            }

            //Calc SubTotal
            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);
            //DeliveryMethod
            var DM = await _deliveryMethodsRepo.GetByIdAsync(deliveryMethod);
            //Create Order
            // DM => deliveryMethod
            var order = new Order(customerEmail, shippingAddress, DM, orderItems, subTotal);
            await _ordersRepo.CreateAsync(order);

            //--- Save in Database ----
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByIdForUserAsync(string orderId, string customerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrderForUserAsync(string customerEmail)
        {
            throw new NotImplementedException();
        }
    }
}

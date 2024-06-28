using Talabat.core.Entities;
using Talabat.core.Entities.OrderAggregate;
using Talabat.core.Repositorires;
using Talabat.core.Services;
using Talabat.core.Specifications;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IGenericRepository<Product> _productRepo;
        //private readonly IGenericRepository<DeliveryMethod> _deliveryMethodsRepo;
        //private readonly IGenericRepository<Order> _ordersRepo;

        public OrderService(ICartRepository cartRepository,
            IUnitOfWork unitOfWork)
        //, IGenericRepository<Product> productRepo,
        //IGenericRepository<DeliveryMethod> DeliveryMethodsRepo,
        //IGenericRepository<Order> OrdersRepo)
        {
            this._cartRepository = cartRepository;
            this._unitOfWork = unitOfWork;
            //this._productRepo = productRepo;
            //this._deliveryMethodsRepo = DeliveryMethodsRepo;
            //this._ordersRepo = OrdersRepo;
        }

        public async Task<Order> CreateOrderAsync(string customerEmail, string cartId, int deliveryMethod, Address shippingAddress)
        {
            // Retrieve the cart by ID
            var cart = await _cartRepository.GetCartAsync(cartId);
            if (cart == null)
            {
                throw new Exception("Cart not found.");
            }


            var orderItems = new List<OrderItem>();
            foreach (var item in cart.Items)
            {
                // Retrieve product details from the database
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                if (product == null)
                {
                    throw new Exception($"Product with ID {item.Id} not found.");
                }

                //To Ensure Information From DB
                var productitemordered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
                var orderitem = new OrderItem(productitemordered, product.Price, item.Quantity);

                ///Add To list
                orderItems.Add(orderitem);
            }

            //Calc SubTotal
            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);
            // Retrieve the delivery method by ID
            //DeliveryMethod
            var DM = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethod);
            if (DM == null)
            {
                throw new Exception("Delivery method not found.");
            }
            //Create Order

            // DM => deliveryMethod
            var order = new Order(customerEmail, shippingAddress, DM, orderItems, subTotal);
            await _unitOfWork.Repository<Order>().CreateAsync(order);

            //--- Save in Database ----

            var result = await _unitOfWork.Complete();
            if (result <= 0)
                return null;

            return order;
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByIdForUserAsync(string orderId, string customerEmail)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string customerEmail)
        {

            var spec = new OrderWithItemsandDMSpecifications(customerEmail);

            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);

            return (IReadOnlyList<Order>)orders;


        }
    }
}

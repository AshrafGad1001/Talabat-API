using Talabat.core.Entities.OrderAggregate;

namespace Talabat.APIs.DTOs
{
    public class OrderDTO
    {
        public string CartId { get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressDTO  ShippingAddress{ get; set; }
    }
}

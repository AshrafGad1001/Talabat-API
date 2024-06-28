using Talabat.core.Entities.OrderAggregate;

namespace Talabat.APIs.DTOs
{
    public class OrderReturnDTO
    {
        public int Id { get; set; }
        public string CustomerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } 

        public string Status { get; set; }

        public Address ShippingAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal DeliveryMethodCost { get; set; }



        public ICollection<OrderItemDTO> Items { get; set; }

        public string? PaymentIntendId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }

     


    }
}

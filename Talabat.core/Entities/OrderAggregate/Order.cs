namespace Talabat.core.Entities.OrderAggregate
{
    public class Order : BaseEntity
    {
        public string CustomerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public Address ShippingAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public ICollection<OrderItem> Items { get; set; }
        public string PaymentIntendId { get; set; }

        public decimal SubTotal { get; set; }

        public decimal GetTotal()
            => SubTotal + DeliveryMethod.Cost;


        public Order()
        {

        }
        public Order(string customerEmail, Address shippingAddress,
            DeliveryMethod deliveryMethod, ICollection<OrderItem> items,
            decimal subTotal)
        {
            this.CustomerEmail = customerEmail;
            this.ShippingAddress = shippingAddress;
            this.DeliveryMethod = deliveryMethod;
            this.Items = items;
            this.SubTotal = subTotal;
        }
    }
}

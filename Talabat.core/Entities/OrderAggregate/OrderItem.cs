namespace Talabat.core.Entities.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public ProductItemOrdered Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}

namespace Talabat.core.Entities.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public ProductItemOrdered Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public OrderItem()
        {
        }
        public OrderItem(ProductItemOrdered product, decimal price, int quantity)
        {
            this.Product = product;
            this.Price = price;
            this.Quantity = quantity;
        }
    }
}

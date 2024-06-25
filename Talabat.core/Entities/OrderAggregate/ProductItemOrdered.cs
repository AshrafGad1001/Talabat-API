namespace Talabat.core.Entities.OrderAggregate
{
    public class ProductItemOrdered
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public ProductItemOrdered()
        {

        }
        public ProductItemOrdered(int productId, string productName, string pictureUrl)
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.PictureUrl = pictureUrl;
        }
    }
}

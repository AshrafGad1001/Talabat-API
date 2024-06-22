namespace Talabat.core.Entities
{
    public class CustomerCart
    {
        public string Id { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public CustomerCart(string id)
        {
            this.Id = id;
        }
    }
}

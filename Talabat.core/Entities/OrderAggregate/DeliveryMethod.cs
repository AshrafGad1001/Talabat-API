namespace Talabat.core.Entities.OrderAggregate
{
    public class DeliveryMethod : BaseEntity
    {
        public string ShortName { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public string DeliveryTime { get; set; }
    }
}

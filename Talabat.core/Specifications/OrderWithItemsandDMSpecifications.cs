using Talabat.core.Entities.OrderAggregate;

namespace Talabat.core.Specifications
{
    public class OrderWithItemsandDMSpecifications : BaseSpecification<Order>
    {
        public OrderWithItemsandDMSpecifications(string customerEmail) : base(O => O.CustomerEmail == customerEmail)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);


            AddOrderByDescending(O => O.OrderDate);
        }
        public OrderWithItemsandDMSpecifications(int orderId, string customerEmail)
                        : base(O => O.CustomerEmail == customerEmail && O.Id == orderId)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
        }
    }
}

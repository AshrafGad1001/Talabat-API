using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.core.Entities.OrderAggregate;

namespace Talabat.Repository.Data.Config
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(orderitem => orderitem.Product, OI => OI.WithOwner());

            builder.Property(OI => OI.Price)
                .HasColumnType("decimal(18.2)");

        }
    }
}

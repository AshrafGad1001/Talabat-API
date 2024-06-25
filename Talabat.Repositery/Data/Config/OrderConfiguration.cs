using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.core.Entities.OrderAggregate;

namespace Talabat.Repository.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            //////WithOwner  -> public Addrress ShippingAddress [To =>] Base Componant
            builder.OwnsOne(O => O.ShippingAddress, C => C.WithOwner());

            builder.Property(O => O.Status)
                    .HasConversion(
                            ////Store In DB As String From Enum 
                            OStatus => OStatus.ToString(),
                            ////Retreive  As Enum
                            OStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus)
                      );

            builder.HasMany(O=>O.Items).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}

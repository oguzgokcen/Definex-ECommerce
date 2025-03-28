
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Final.Infrustructure.Data.EntityConfigurations;

internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasOne(x => x.Order)
			.WithMany(o => o.OrderItems)
			.HasForeignKey(x => x.OrderId);

	}
}

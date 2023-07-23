using EdrakTask.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EdrakTask.Infrastructure.Database.Configurations
{
    internal class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.HasKey(o => o.Id);

            builder.HasData(new OrderStatus()
            {
                Id = 1,
                Name = "Pending",
            });

            builder.HasData(new OrderStatus()
            {
                Id = 2,
                Name = "Shipped",
            });

            builder.HasData(new OrderStatus()
            {
                Id = 3,
                Name = "Delivered",
            });

            builder.ToTable("OrderStatus");
        }
    }
}

using EdrakTask.Core.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EdrakTask.Core.Enums;

namespace EdrakTask.Infrastructure.Database.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(c => c.CustomerId).IsRequired();
            //builder.Property(c => c.Status).HasDefaultValue((int)OrderStatusEnum.Pending);

            builder.ToTable("Orders");
        }
    }
}

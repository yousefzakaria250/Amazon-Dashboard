using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ITI.ElectroDev.Models
{
    public class OrderItemsConfig : IEntityTypeConfiguration<OrderItems>
    {
        public void Configure(EntityTypeBuilder<OrderItems> builder)
        {

            builder.ToTable("OrderItems");
            builder.HasKey(i => new { i.OrderId, i.ProductId });
            //builder.HasKey(i => i.OrderId);
            //builder.HasKey(i => i.ProductId);
           // builder.Property(i => i.OrderId).IsRequired();
            builder.Property(i => i.Quantity).IsRequired();
            builder.Property(i => i.Price).IsRequired();
           // builder.Property(i => i.ProductId).IsRequired();
        }
    }
}

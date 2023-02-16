using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.ElectroDev.Models
{
    public static class DbRelations
    {
        public static void MapRelations(this ModelBuilder builder)
        {
            builder.Entity<Product>()
                .HasOne(i => i.Brand).WithMany(i => i.Products).HasForeignKey(i => i.BrandId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Brand>()
                .HasOne(i => i.Category).WithMany(i => i.Brands).HasForeignKey(i => i.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ProductImages>()
                .HasOne(i => i.Product).WithMany(i => i.ProductImages).HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);


    
            builder.Entity<OrderItems>()
                .HasOne(i => i.OrderDetails).WithMany(i => i.OrderItems).HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<OrderDetails>()
                .HasOne(i => i.User).WithMany(i => i.OrderDetails)
                .HasForeignKey(i => i.UserId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<OrderItems>()
                .HasOne(i => i.Product).WithMany(i => i.OrderItems)
                .HasForeignKey(i => i.ProductId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}


using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.ElectroDev.Models
{
    public class Context:IdentityDbContext<User>
    {
       
        public Context(DbContextOptions options) : base(options)
        { }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductImages> productImages { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new BrandConfig().Configure(modelBuilder.Entity<Brand>());
            new CategoryConfig().Configure(modelBuilder.Entity<Category>());
            new ProductConfig().Configure(modelBuilder.Entity<Product>());
            new ProductImagesConfig().Configure(modelBuilder.Entity<ProductImages>());
            new OrderDetailsConfig().Configure(modelBuilder.Entity<OrderDetails>());
            new OrderItemsConfig().Configure(modelBuilder.Entity<OrderItems>());
            modelBuilder.MapRelations();
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            base.OnConfiguring(optionsBuilder);
        }

    }
}

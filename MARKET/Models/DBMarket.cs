using MARKET.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MARKET.Models
{
    public class DBMarket:IdentityDbContext
    {
        public DBMarket(DbContextOptions<DBMarket> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductPayment>().HasKey(pp => new { pp.PaymentId, pp.ProductId });
            modelBuilder.Entity<ProductTag>().HasKey(pp => new { pp.TagId, pp.ProductId });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProductPayment> ProductPayment { get; set; }
        public DbSet<ProductTag> ProductTag { get; set; }
    }
}

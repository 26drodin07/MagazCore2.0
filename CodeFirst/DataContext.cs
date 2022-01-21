using CodeFirst.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace CodeFirst
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<User>().HasKey(x => x.Id);
        }
      


        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PropCat> Props_Cat { get; set; }
        public DbSet<PropProd> Props_Prod { get; set; }
        public DbSet<Shop_Prod> Shop_Prods { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }

    }
}

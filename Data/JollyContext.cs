using Microsoft.EntityFrameworkCore;
using Venna.Models;

namespace Venna.Data;

public class JollyContext : DbContext
{

    public JollyContext(DbContextOptions options) : base(options)
    {
    }
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer(@"Server=./;Database=Test;Trusted_connection=true;encrypt=false;");
    //}
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<SubCategory>().HasMany(x => x.products)
    //        .WithOne(x => x.SubCategory).OnDelete(DeleteBehavior.Restrict);
    //}


    public  DbSet<User> Users { get; set; } 
    public  DbSet<Review> Review { get; set; }
    public  DbSet<Product> Product { get; set; }
    public  DbSet<Orderitems> Orderitems { get; set; }
    public  DbSet<Order> Order { get; set; }
    public  DbSet<Category> Category { get; set; }
    public  DbSet<Cart> Cart { get; set; }
    public  DbSet<Cartitems> Cartitems { get; set; }
    public  DbSet<Brand> Brand { get; set; }
    public  DbSet<MainImage> Mainimg { get; set; }
    public DbSet<SubCategory> SubCategorys { get; set; }
    public DbSet<CategoryCover> CategoryCover { get; set; }
    public DbSet<SubCover> SubCovers { get; set; } 
}
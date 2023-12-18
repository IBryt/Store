using IgorBryt.Store.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IgorBryt.Store.DAL.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Products = Set<Product>();
        ProductCategories = Set<ProductCategory>();
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
}

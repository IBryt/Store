using IgorBryt.Store.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IgorBryt.Store.DAL.Data;

public class AppDbContext : DbContext
{

    public AppDbContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Store;Trusted_Connection=True;");
    }


    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
}

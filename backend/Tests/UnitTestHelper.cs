using IgorBryt.Store.DAL.Data;
using IgorBryt.Store.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IgorBryt.Store.Tests;

internal static class UnitTestHelper
{
    public static DbContextOptions<AppDbContext> GetUnitTestDbOptions()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using (var context = new AppDbContext(options))
        {
            SeedData(context);
        }

        return options;
    }

    public static void SeedData(AppDbContext context)
    {
        context.ProductCategories.AddRange(ExpectedCategory);
        context.Products.AddRange(ExpectedProducts);
        context.SaveChanges();
    }

    public static IEnumerable<Product> ExpectedProducts =>
           new[]
           {
                new Product { Id = 1, ProductCategoryId = 1, ProductName = "Xiaomi 7", Price = 8_000, Description = "Xiaomi description1" },
                new Product { Id = 2, ProductCategoryId = 1, ProductName = "Lenovo", Price = 20_002 , Description = "Lenovo description2" },
                new Product { Id = 3, ProductCategoryId = 2, ProductName = "Lenovo3", Price = 20_003, Description = "Lenovo description3" },
                new Product { Id = 4, ProductCategoryId = 2, ProductName = "Lenovo4", Price = 20_004 , Description = "Lenovo description4" },
                new Product { Id = 5, ProductCategoryId = 2, ProductName = "Lenovo5", Price = 20_005 , Description = "Lenovo description5" },
                new Product { Id = 6, ProductCategoryId = 2, ProductName = "Lenovo6", Price = 20_006 , Description = "Lenovo description6" },
                new Product { Id = 7, ProductCategoryId = 2, ProductName = "Lenovo7", Price = 20_007 , Description = "Lenovo description7" },
                new Product { Id = 8, ProductCategoryId = 2, ProductName = "Lenovo8", Price = 20_008 , Description = "Lenovo description8" },
                new Product { Id = 9, ProductCategoryId = 2, ProductName = "Lenovo9", Price = 20_009 , Description = "Lenovo description9" },
                new Product { Id = 10, ProductCategoryId = 2, ProductName = "Lenovo10", Price = 20_010 , Description = "Lenovo description10" },
                new Product { Id = 11, ProductCategoryId = 2, ProductName = "Lenovo11", Price = 20_011 , Description = "Lenovo description11" },
                new Product { Id = 12, ProductCategoryId = 2, ProductName = "Lenovo12", Price = 20_012 , Description = "Lenovo description12" },
                new Product { Id = 13, ProductCategoryId = 2, ProductName = "Lenovo13", Price = 20_013 , Description = "Lenovo description13" },
           };

    public static IEnumerable<ProductCategory> ExpectedCategory =>
         new[]
           {
                new ProductCategory { Id = 1, CategoryName = "Phones" },
                new ProductCategory { Id = 2, CategoryName = "Laptop" }
           };

    public static IEnumerable<Product> ExpectedProductsWithDetails() =>
         ExpectedProducts
            .Join(ExpectedCategory,
                  product => product.ProductCategoryId,
                  category => category.Id,
                  (product, category) => new Product
                  {
                      Id = product.Id,
                      ProductName = product.ProductName,
                      Price = product.Price,
                      Description = product.Description,
                      Category = category,
                      ProductCategoryId = category.Id
                  });

}

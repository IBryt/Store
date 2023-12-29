using IgorBryt.Store.DAL.Data;
using IgorBryt.Store.DAL.Entities;
using IgorBryt.Store.DAL.Repositories;
using NUnit.Framework;
using static IgorBryt.Store.Tests.EqualityComparer;

namespace IgorBryt.Store.Tests.DAL;

[TestFixture]
public class IRepositoryTests
{
    [TestCase(1)]
    [TestCase(2)]
    public async Task ProductRepository_GetByIdAsync_ReturnsSingleValue(int id)
    {
        using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var productRepository = new ProductRepository(context);

        var product = await productRepository.GetByIdAsync(id);

        var expected = UnitTestHelper.ExpectedProducts.FirstOrDefault(x => x.Id == id);

        Assert.That(product, Is.EqualTo(expected).Using(new ProductEqualityComparer()), message: "GetByIdAsync method works incorrect");
    }

    [TestCase(33)]

    public async Task ProductRepository_GetByIdAsync_ReturnsNullIfNotExist(int id)
    {
        using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var productRepository = new ProductRepository(context);

        var product = await productRepository.GetByIdAsync(id);

        var expected = UnitTestHelper.ExpectedProducts.FirstOrDefault(x => x.Id == id);

        Assert.That(product, Is.EqualTo(null), message: "GetByIdAsync method works incorrect");
    }

    [Test]
    public async Task ProductRepository_AddAsync_AddsValueToDatabase()
    {
        using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var productRepository = new ProductRepository(context);
        var product = new Product { Id = 33, ProductName = "test", Description = "test" };

        await productRepository.AddAsync(product);
        await context.SaveChangesAsync();

        Assert.That(context.Products.Count(), Is.EqualTo(14), message: "AddAsync method works incorrect");
    }

    [TestCase(1, 12)]
    [TestCase(33, 13)]
    public async Task ProductRepository_DeleteByIdAsync_DeletesEntityIfExist(int id, int expectedCount)
    {
        using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var productRepository = new ProductRepository(context);

        await productRepository.DeleteByIdAsync(id);
        await context.SaveChangesAsync();

        Assert.That(context.Products.Count(), Is.EqualTo(expectedCount), message: "DeleteByIdAsync works incorrect");
    }


    [TestCase(1, 12)]
    public async Task ProductRepository_Delete_DeletesEntityIfExist(int productId, int expectedCount)
    {
        using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var productRepository = new ProductRepository(context);
        var product = new Product { Id = productId , ProductName = "test name", Description = "new desc"};
        productRepository.Delete(product);
        await context.SaveChangesAsync();

        Assert.That(context.Products.Count(), Is.EqualTo(expectedCount), message: "Delete works incorrect");
    }

    [Test]
    public async Task ProductRepository_Update_UpdatesEntity()
    {
        using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var productRepository = new ProductRepository(context);
        var product = new Product
        {
            Id = 1,
            ProductCategoryId = 1,
            ProductName = "test name",
            Price = 30,
            Description = "new desc",
            ImageUrl= "new imageUrl",
        };

        productRepository.Update(product);
        await context.SaveChangesAsync();

        Assert.That(product, Is.EqualTo(new Product
        {
            Id = 1,
            ProductCategoryId = 1,
            ProductName = "test name",
            Price = 30,
            Description = "new desc",
            ImageUrl = "new imageUrl",
        }).Using(new ProductEqualityComparer()), message: "Update method works incorrect");
    }
}

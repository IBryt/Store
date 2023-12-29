using IgorBryt.Store.DAL.Data;
using IgorBryt.Store.DAL.Data.Common;
using IgorBryt.Store.DAL.Repositories;
using NUnit.Framework;
using static IgorBryt.Store.Tests.EqualityComparer;

namespace IgorBryt.Store.Tests.DAL;

[TestFixture]
public class ProductRepositoryTests
{


    [TestCase(null, null, 12)]
    [TestCase(null, 2, 1)]
    [TestCase(1, null, 2)]
    [TestCase(2, null, 11)]
    public async Task ProductRepository_GetProductsWithDetailsAsync_ReturnsProducts(int? categoryId, int? page, int expectedCount)
    {
        using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var productRepository = new ProductRepository(context);
        var products = await productRepository.GetProductsWithDetailsAsync(new ProductPagingOptions { CategoryId = categoryId, Page = page });

        Assert.That(products.Count, Is.EqualTo(expectedCount), message: "GetProductsWithDetailsAsync method works incorrect");
    }

    [TestCase(null, 2)]
    [TestCase(2,1)]
    public async Task ProductRepository_GetCountAsync_ReturnsCountPage(int? categoryId, int expectedCount)
    {
        using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var productRepository = new ProductRepository(context);
        var count = await productRepository.GetCountPageAsync(new ProductPagingOptions { CategoryId = categoryId });

        Assert.That(count, Is.EqualTo(expectedCount), message: "GetCountAsync method works incorrect");
    }


    [TestCase(1)]
    [TestCase(2)]
    public async Task ProductRepository_GetProductWithDetailsByIdAsync_ReturnsSingleValueWithDetails(int id)
    {
        using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var productRepository = new ProductRepository(context);
        var product = await productRepository.GetProductWithDetailsByIdAsync(id);

        var expected = UnitTestHelper.ExpectedProductsWithDetails().Single(p => p.Id == id);
        Assert.That(product, Is.EqualTo(expected).Using(new ProductEqualityComparer()), message: "GetProductWithDetailsByIdAsync method works incorrect");
    }
}

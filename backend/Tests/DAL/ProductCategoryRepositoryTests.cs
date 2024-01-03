using IgorBryt.Store.DAL.Data;
using IgorBryt.Store.DAL.Repositories;
using NUnit.Framework;
using static IgorBryt.Store.Tests.EqualityComparer;

namespace IgorBryt.Store.Tests.DAL;

[TestFixture]
public class ProductCategoryRepositoryTests
{
    [Test]
    public async Task ProductCategoryRepository_GetAllAsync_ReturnsAllValues()
    {
        using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var productCategoryRepository = new ProductCategoryRepository(context);
        var productCategories = await productCategoryRepository.GetAllAsync();

        Assert.That(productCategories, Is.EqualTo(UnitTestHelper.ExpectedCategory).Using(new ProductCategoryEqualityComparer()), message: "GetAllAsync method works incorrect");
    }
}

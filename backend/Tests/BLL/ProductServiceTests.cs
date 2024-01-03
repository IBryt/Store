using Business.Validation;
using FluentAssertions;
using FluentValidation;
using IgorBryt.Store.BLL;
using IgorBryt.Store.BLL.Models;
using IgorBryt.Store.BLL.Services;
using IgorBryt.Store.DAL.Data;
using IgorBryt.Store.DAL.Interfaces;
using IgorBryt.Store.DAL.Repositories;
using Moq;
using NUnit.Framework;

namespace IgorBryt.Store.Tests.BLL;

public class ProductServiceTests
{
    private ProductService _productService;
    private AppDbContext _appDbContext;

    [SetUp]
    public void Init()
    {
        _appDbContext = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());
        _productService = GetProductService();
    }

    [TearDown]
    public void Cleanup()
    {
        _appDbContext.Dispose();
    }

    [Test]
    public async Task ProductService_AddAsync_ThrowsStoreExceptionWithEmptyProductName()
    {
        var product = new ProductModel { Id = 124, ProductName = "", ProductCategoryId = 1, Price = 29.00m, Description = "Description" };

        Func<Task> act = async () => await _productService.AddAsync(product);

        await act.Should().ThrowAsync<StoreException>();
    }

    [Test]
    public async Task ProductService_AddAsync_ThrowsStoreExceptionIfPriceIsNegative()
    {
        var product = new ProductModel { Id = 124, ProductName = "ProductName", ProductCategoryId = 1, Price = -29.00m, Description = "Description" };

        Func<Task> act = async () => await _productService.AddAsync(product);

        await act.Should().ThrowAsync<StoreException>();
    }

    [Test]
    public async Task ProductService_AddCategoryAsync_ThrowsStoreExceptionWithEmptyCategoryName()
    {
        var product = new ProductModel { Id = 124, CategoryName = "", ProductCategoryId = 1, Price = -29.00m, Description = "Description" };

        Func<Task> act = async () => await _productService.AddAsync(product);

        await act.Should().ThrowAsync<StoreException>();
    }

    [Test]
    public async Task ProductService_AddCategoryAsync_ThrowsStoreExceptionWithEmptyDescription()
    {
        var product = new ProductModel { Id = 124, CategoryName = "", ProductCategoryId = 1, Price = -29.00m, Description = "" };

        Func<Task> act = async () => await _productService.AddAsync(product);

        await act.Should().ThrowAsync<StoreException>();
    }


    [TestCase(1, 1)]
    [TestCase(null, 2)]
    public async Task ProductService_GetCountPageAsync_GetCount(int? CategoryId, int expected)
    {
        var filterModel = new FilterProductModel { CategoryId = CategoryId, };

        var actual = await _productService.GetCountPageAsync(filterModel);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase(null, null)]
    [TestCase(null, 2)]
    [TestCase(1, null)]
    [TestCase(2, null)]
    public async Task ProductService_GetProductsAsync_Returns_ProductModels(int? CategoryId, int? page)
    {
        var expected = UnitTestHelper.ExpectedProductModelsWithCategory;
        if (CategoryId != null)
        {
            expected = expected.Where(p => p.ProductCategoryId == CategoryId);
        }

        if (page != null)
        {
            int skip = (page.Value - 1) * 12;
            expected = expected.Skip(skip);
        }

        var filterModel = new FilterProductModel { CategoryId = CategoryId, Page = page};
        var actual = await _productService.GetProductsAsync(filterModel);

        actual.Should().BeEquivalentTo(expected.Take(12));
    }


    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    public async Task ProductService_GetProductWithDetailsByIdAsync_Returns_ProductModel(int id)
    {
        var expected = UnitTestHelper.ExpectedProductModelsWithCategory.FirstOrDefault(p => p.Id == id);

        var actual = await _productService.GetProductWithDetailsByIdAsync(id);

        actual.Should().BeEquivalentTo(expected);
    }

    private ProductService GetProductService()
    {
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(u => u.ProductRepository).Returns(new ProductRepository(_appDbContext));

        var productService = new ProductService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), new ProductValidator());

        return productService;
    }
}

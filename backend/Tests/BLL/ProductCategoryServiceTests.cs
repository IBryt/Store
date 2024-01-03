using FluentAssertions;
using IgorBryt.Store.BLL.Services;
using IgorBryt.Store.BLL.Validation;
using IgorBryt.Store.DAL.Data;
using IgorBryt.Store.DAL.Interfaces;
using IgorBryt.Store.DAL.Repositories;
using Moq;
using NUnit.Framework;

namespace IgorBryt.Store.Tests.BLL;

public class ProductCategoryServiceTests
{
    private ProductCategoryService _productCategoryService;
    private AppDbContext _appDbContext;

    [SetUp]
    public void Init()
    {
        _appDbContext = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());
        _productCategoryService = GetProductCategoryService();
    }

    [TearDown]
    public void Cleanup()
    {
        _appDbContext.Dispose();
    }

    public async Task ProductCategoryService_GetAllAsyncc_Returns_ProductCategoryModels()
    {
        var actual = await _productCategoryService.GetAllAsync();
        actual.Should().BeEquivalentTo(UnitTestHelper.ExpectedCategoryModels);
    }

    private ProductCategoryService GetProductCategoryService()
    {
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(u => u.ProductCategoryRepository).Returns(new ProductCategoryRepository(_appDbContext));

        var productCategoryService = new ProductCategoryService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), new ProductCategoryValidator());

        return productCategoryService;
    }
}

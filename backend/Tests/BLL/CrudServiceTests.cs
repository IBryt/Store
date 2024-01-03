using FluentAssertions;
using FluentValidation;
using IgorBryt.Store.BLL.Models;
using IgorBryt.Store.BLL.Services;
using IgorBryt.Store.DAL.Entities;
using IgorBryt.Store.DAL.Interfaces;
using Moq;
using NUnit.Framework;

namespace IgorBryt.Store.Tests.BLL;

public class CrudServiceTests
{
    [TestCase(1)]
    [TestCase(2)]
    public async Task ProductService_GetByIdAsync_ReturnsProductModel(int id)
    {
        var expected = UnitTestHelper.ExpectedProductModels.FirstOrDefault(x => x.Id == id);

        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork
            .Setup(x => x.ProductRepository.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(UnitTestHelper.ExpectedProducts.FirstOrDefault(x => x.Id == id));

        var productService = new ProductService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), new InlineValidator<ProductModel>());


        var actual = await productService.GetByIdAsync(id);

        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task ProductService_AddAsync_AddsProduct()
    {
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(m => m.ProductRepository.AddAsync(It.IsAny<Product>()));

        var productService = new ProductService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), new InlineValidator<ProductModel>());
        var product = new ProductModel { Id = 124, ProductName = "phone", ProductCategoryId = 1, Price = 29.00m, Description = "Description" };

        await productService.AddAsync(product);

        mockUnitOfWork.Verify(x => x.ProductRepository.AddAsync(It.Is<Product>(c => c.Id == product.Id && c.ProductCategoryId == product.ProductCategoryId && c.Price == product.Price && c.ProductName == product.ProductName)), Times.Once);
        mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
    }

    [Test]
    public async Task ProductService_UpdateAsync_UpdatesProduct()
    {
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(m => m.ProductRepository.Update(It.IsAny<Product>()));

        var productService = new ProductService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), new InlineValidator<ProductModel>());
        var product = new ProductModel { Id = 92, ProductName = "Cabrales", ProductCategoryId = 1, CategoryName = "Household", Price = 29.00m, Description = "Description" };

        await productService.UpdateAsync(product);

        mockUnitOfWork.Verify(x => x.ProductRepository.Update(It.Is<Product>(c => c.Id == product.Id && c.ProductCategoryId == product.ProductCategoryId && c.Price == product.Price && c.ProductName == product.ProductName)), Times.Once);
        mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
    }

    [TestCase(1)]
    [TestCase(2)]
    public async Task ProductService_DeleteAsync_DeletesProduct(int id)
    {
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(m => m.ProductRepository.DeleteByIdAsync(It.IsAny<int>()));
        var productService = new ProductService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), new InlineValidator<ProductModel>());

        await productService.DeleteAsync(id);

        mockUnitOfWork.Verify(x => x.ProductRepository.DeleteByIdAsync(id), Times.Once);
        mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
    }
}

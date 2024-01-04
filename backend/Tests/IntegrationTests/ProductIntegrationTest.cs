using FluentAssertions;
using FluentValidation;
using IgorBryt.Store.BLL.Models;
using Library.Tests.IntegrationTests;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Text;

namespace IgorBryt.Store.Tests.IntegrationTests;

public class ProductIntegrationTest
{
    private CustomWebApplicationFactory _factory;
    private HttpClient _client;
    private const string RequestUri = "api/products";

    [SetUp]
    public void Init()
    {
        _factory = new CustomWebApplicationFactory();
        _client = _factory.CreateClient();
    }

    [Test]
    public async Task ProductsController_GetProducts_Returns_ProductModels()
    {
        var queryParams = "";
        var expected = UnitTestHelper.ExpectedProductModelsWithCategory.Take(12);

        await CheckGetProducts(queryParams, expected);

        queryParams = "?page=2";
        expected = UnitTestHelper.ExpectedProductModelsWithCategory.Skip(12).Take(12);

        await CheckGetProducts(queryParams, expected);

        queryParams = "?categoryId=1";
        expected = UnitTestHelper.ExpectedProductModelsWithCategory.Where(p => p.ProductCategoryId == 1).Take(12);

        await CheckGetProducts(queryParams, expected);

        queryParams = "?categoryId=2";
        expected = UnitTestHelper.ExpectedProductModelsWithCategory.Where(p => p.ProductCategoryId == 2).Take(12);

        await CheckGetProducts(queryParams, expected);
    }

    [Test]
    public async Task ProductsController_GetPagesCount_Returns_PageCount()
    {
        var queryParams = "";
        var expected = 2;

        await CheckGetPageCount(queryParams, expected);

        queryParams = "?categoryId=1";
        expected = 1;

        await CheckGetPageCount(queryParams, expected);

        queryParams = "?categoryId=2";
        expected = 1;

        await CheckGetPageCount(queryParams, expected);
    }

    [TestCase(1)]
    [TestCase(2)]
    public async Task ProductsController_getProductById_Returns_Products(int id)
    {
        var expected = UnitTestHelper.ExpectedProductModelsWithCategory.Single(p => p.Id == id);
        var httpResponse = await _client.GetAsync($"{RequestUri}/{id}" );

        httpResponse.EnsureSuccessStatusCode();
        var stringResponse = await httpResponse.Content.ReadAsStringAsync();
        var actual = JsonConvert.DeserializeObject<ProductModel>(stringResponse);

        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task ProductsController_getProductByIds_Returns_Products()
    {
        var ids = new int[] { 1, 4};

        var expected = UnitTestHelper.ExpectedProductModelsWithCategory.Where(p => ids.Contains(p.Id));

        var jsonIds = JsonConvert.SerializeObject(ids);
        var content = new StringContent(jsonIds, Encoding.UTF8, "application/json");
        var httpResponse = await _client.PostAsync($"{RequestUri}/ids", content);

        httpResponse.EnsureSuccessStatusCode();
        var stringResponse = await httpResponse.Content.ReadAsStringAsync();
        var actual = JsonConvert.DeserializeObject<IEnumerable<ProductModel>>(stringResponse);

        actual.Should().BeEquivalentTo(expected);
    }

    private async Task CheckGetPageCount(string queryParams, int expected)
    {
        var httpResponse = await _client.GetAsync(RequestUri + "/pagesCount" + queryParams);

        httpResponse.EnsureSuccessStatusCode();
        var stringResponse = await httpResponse.Content.ReadAsStringAsync();
        var actual = JsonConvert.DeserializeObject<int>(stringResponse);

        actual.Should().Be(expected);
    }

    private async Task CheckGetProducts(string queryParams, IEnumerable<ProductModel> expected)
    {
        var httpResponse = await _client.GetAsync(RequestUri + queryParams);

        httpResponse.EnsureSuccessStatusCode();
        var stringResponse = await httpResponse.Content.ReadAsStringAsync();
        var actual = JsonConvert.DeserializeObject<IEnumerable<ProductModel>>(stringResponse);

        actual.Should().BeEquivalentTo(expected);
    }
}

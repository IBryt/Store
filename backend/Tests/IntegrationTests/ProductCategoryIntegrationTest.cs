using FluentAssertions;
using IgorBryt.Store.BLL.Models;
using Library.Tests.IntegrationTests;
using Newtonsoft.Json;
using NUnit.Framework;

namespace IgorBryt.Store.Tests.IntegrationTests;

public class ProductCategoryIntegrationTest
{
    private CustomWebApplicationFactory _factory;
    private HttpClient _client;
    private const string RequestUri = "api/productcategories";

    [SetUp]
    public void Init()
    {
        _factory = new CustomWebApplicationFactory();
        _client = _factory.CreateClient();
    }

    [Test]
    public async Task ProductCategoriesController_GetProductCategories_Returns_Categories()
    {
        var expected = UnitTestHelper.ExpectedCategoryModels;

        var httpResponse = await _client.GetAsync(RequestUri);

        httpResponse.EnsureSuccessStatusCode();
        var stringResponse = await httpResponse.Content.ReadAsStringAsync();
        var actual = JsonConvert.DeserializeObject<IEnumerable<ProductCategoryModel>>(stringResponse);

        actual.Should().BeEquivalentTo(expected);
    }
}

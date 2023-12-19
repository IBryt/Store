using IgorBryt.Store.BLL.Interfaces;
using IgorBryt.Store.BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace IgorBryt.Store.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductCategoryController : Controller
{
    private readonly IProductCategoryService _productCategoryService;

    public ProductCategoryController(IProductCategoryService productCategoryService)
    {
        _productCategoryService = productCategoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductCategoryModel>>> GetProductCategories()
    {
        var categories = await _productCategoryService.GetAllAsync();
        return Ok(categories);
    }
}

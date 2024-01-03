using FluentValidation;
using IgorBryt.Store.BLL.Interfaces;
using IgorBryt.Store.BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace IgorBryt.Store.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ProductsController : Controller
{
    private readonly IProductService _productService;

    public ProductsController(IValidator<ProductModel> productValidator, IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductModel>>> GetProducts([FromQuery] FilterProductModel filter)
    {
        var categories = await _productService.GetProductsAsync(filter);
        return Ok(categories);
    }

    [HttpGet("pagesCount")]
    public async Task<IActionResult> GetPagesCount([FromQuery] FilterProductModel filter)
    {
        var count = await _productService.GetCountPageAsync(filter);
        return Ok(count);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _productService.GetProductWithDetailsByIdAsync(id);
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> GetProductById([FromBody] ProductModel value)
    {
        await _productService.AddAsync(value);
        return Ok(value);
    }
}

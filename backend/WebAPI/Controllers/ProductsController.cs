using FluentValidation;
using IgorBryt.Store.BLL.Interfaces;
using IgorBryt.Store.BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace IgorBryt.Store.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : Controller
{
    private readonly IValidator<ProductModel> _productValidator;
    private readonly IProductService _productService;

    public ProductsController(IValidator<ProductModel> productValidator, IProductService productService)
    {
        _productValidator = productValidator;
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
        var count = await _productService.GetCountAsync(filter);
        return Ok(count);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> getProductById(int id)
    {
        var product = await _productService.GetProductWithDetailsByIdAsync(id);
        return Ok(product);
    }
}

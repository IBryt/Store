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

    [HttpGet("{page}")]
    public async Task<ActionResult<IEnumerable<ProductModel>>> GetProducts([FromQuery] FilterProductModel filter, int page = 1)
    {
        var categories = await _productService.GetProductsAsync(page, filter);
        return Ok(categories);
    }
}


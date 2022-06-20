using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.DTO;
using WebStore.Interfaces.Services;

namespace WebStore.WebAPI.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsApiController : ControllerBase
{
    private readonly IProductData _producData;
    private readonly ILogger<ProductsApiController> _logger;

    public ProductsApiController(IProductData ProducData, ILogger<ProductsApiController> Logger)
    {
        _producData = ProducData;
        _logger = Logger;
    }

    [HttpGet("sections")]
    public IActionResult GetSections() => Ok(_producData.GetSections().ToDTO());

    [HttpGet("brands")]
    public IActionResult GetBrands() => Ok(_producData.GetBrands().ToDTO());

    [HttpGet("sections/{Id:int}")]
    public IActionResult GetSectionById(int Id) => _producData.GetSectionById(Id) is { } section
        ? Ok(section.ToDTO())
        : NotFound(new { Id });

    [HttpGet("brands/{Id:int}")]
    public IActionResult GetBrandById(int Id) => _producData.GetSectionById(Id) is { } brand
       ? Ok(brand.ToDTO())
       : NotFound(new { Id });

    [HttpPost]
    public IActionResult GetProduct([FromBody] ProductFilter filter)
    {
        var products = _producData.GetProducts(filter);
        
        if(products.Any())
            return Ok(products.ToDTO());
        return NoContent();
    }

    [HttpGet("{Id}")]
    public IActionResult GetProductById(int Id) => _producData.GetProductById(Id) is { } product
        ? Ok(product.ToDTO())
        : NoContent();
}

using Microsoft.AspNetCore.Mvc;
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
}

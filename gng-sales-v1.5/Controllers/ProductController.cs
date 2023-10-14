using Microsoft.AspNetCore.Mvc;
using Services.Leyer.Services.ProductService;
using Services.Leyer.ViewModels.Product;

namespace gng_sales_v1._5.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _product;
    public ProductController( IProductRepository product )
    {
        _product = product;
    }

    [HttpGet]
    [Route("GetAllProduct")]
    public async Task<IActionResult> GetAllProduct()
    {
        if(!ModelState.IsValid)
        {
            return NotFound(ModelState);
        }

        var response = await _product.GetAllProduct();
        if (response.ErrorCode < 0)
            return NotFound(response);

        return Ok(response);
    }

    [HttpGet]
    [Route("GetProductById/{proId}")]
    public async Task<IActionResult> GetProductByID( int proId)
    {
        var response = await _product.GEtProductById(proId);
        if (response.ErrorCode < 0)
            return NotFound(response);

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductVm productVm)
    {
        if (!ModelState.IsValid)
        {
            return NotFound(ModelState);
        }

        var response = await _product.UpdateProduct(productVm);
        if (response.ErrorCode < 0)
            return NotFound(response);

        return Ok(response);
    }

    [HttpDelete]
    [Route("{proId}")]
    public async Task<IActionResult> DeleteProduct(int proId)
    {
        var response = await _product.DeleteProductByID(proId);
        if (response.ErrorCode < 0)
            return NotFound(response);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductVm productVm)
    {
        if (!ModelState.IsValid)
            return NotFound(ModelState);

        var response  = await _product.CreateProduct(productVm);
        if (response.ErrorCode < 0)
            return NotFound(response);

        return Ok(response);
    }
}

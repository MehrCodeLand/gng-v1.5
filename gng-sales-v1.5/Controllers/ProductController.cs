using Azure;
using Data.Leyer.Models.Structs;
using Data.Leyer.Models.ViewModels.Product;
using goolrang_sales_v1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Leyer.Services.ProductService;

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
    [Route("getProductById/{proId}")]
    public async Task<IActionResult> GetProductByID( int proId)
    {
        var response = await _product.GEtProductById(proId);
        if (response.ErrorCode < 0)
            return NotFound(response.ErrorMessage);


        return Ok(response.Data);
    }

    [HttpPut]
    [Route("updateProduct")]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductVm productVm)
    {
        var response = await _product.UpdateProduct(productVm);
        if (response.ErrorCode < 0)
            return NotFound(response.ErrorMessage);


        return Ok(response);
    }

    [HttpDelete]
    [Route("deleteProduct/{proId}")]
    public async Task<IActionResult> DeleteProduct(int proId)
    {
        var response = await _product.DeleteProductByID(proId);
        if (response.ErrorCode < 0)
            return NotFound(response.ErrorMessage);

        return Ok(response.Message);
    }

    [HttpGet]
    [Route("getAllProduct")]
    public async Task<IActionResult> GetAllProduct()
    {
        var response = await _product.GetAllProduct();
        if (response.ErrorCode < 0)
            return NotFound(response.ErrorMessage);



        return Ok(response.Data);
    }

    [HttpPost]
    [Route("createProduct")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductVm productVm)
    {
        var responce  = await _product.CreateProduct(productVm);
        if (responce.ErrorCode < 0)
            return NotFound(responce.ErrorMessage);

        return Ok(responce.Message);
    }
}

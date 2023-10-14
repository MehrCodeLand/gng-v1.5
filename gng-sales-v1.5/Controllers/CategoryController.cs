using goolrang_sales_v1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.Leyer.Services.CategoryServices;
using Services.Leyer.ViewModels.ViewModels.Category;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace gng_sales_v1._5.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _catService;
    public CategoryController(ICategoryRepository categoryService)
    {
        _catService = categoryService;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var response = await _catService.GetAllCategory();
        if(response.HasError)
        {
            return NotFound(response);
        }

        return Ok(response);
    }

    [HttpGet]
    [Route("GetByName/{name}")]
    public async Task<IActionResult> GetCategoryByName(string name)
    {
        var response = await _catService.GetCategoryByName(name);
        if(response.HasError )
        {
            return NotFound(response);
        }

        return Ok(response); 
    }

    [HttpDelete]
    [Route("{catId}")]
    public async Task<IActionResult> DeleteCAtegoryById(int catId)
    {
        var responce = await _catService.DeleteCategory(catId);

        if(responce.HasError)
        {
            return NotFound( responce );
        }

        return Ok(responce);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory( [FromBody]CreateCategoryVm categoryVm )
    {
        if (!ModelState.IsValid)
            return NotFound(ModelState);

        var response =await _catService.CreateCategory(categoryVm);
        if(response.HasError)
        {
            return NotFound(response);
        }

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCategory([FromBody]UpdateCategoryVm categoryVm)
    {
        if (!ModelState.IsValid)
            return NotFound(ModelState);

        var response = await _catService.UpdateCategory(categoryVm);
        if(response.HasError)
        {
            return NotFound(response);
        }

        return Ok(response);
    }
}

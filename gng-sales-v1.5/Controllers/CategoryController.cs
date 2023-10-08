using Data.Leyer.Models.Structs;
using goolrang_sales_v1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.Leyer.Services.CategoryServices;
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






    [HttpDelete]
    [Route("deleteCategoryById/{catId}")]
    public async Task<IActionResult> DeleteCAtegoryById(int catId)
    {
        var responce = await _catService.DeleteCategory(catId);

        if(responce.ErrorCode  < 0)
        {
            return NotFound( responce.ErrorMessage);
        }


        return Ok(responce.Message + " - DONE");
    }

    [HttpGet]
    [Route("getAll")]
    public async Task<IActionResult> GetAll()
    {
        var res = await _catService.GetAllCategory();
        if(res.ErrorCode < 0)
        {
            return NotFound(res.ErrorCode + " " + res.ErrorMessage );
        }

        return Ok(res.Data);
    }

    [HttpGet]
    [Route("getByName/{name}")]
    public async Task<IActionResult> GetCategoryByName(string name)
    {
        var response = await _catService.GetCategoryByName(name);
        if(response.ErrorCode < 0 )
        {
            return NotFound(response.ErrorMessage);
        }

        return Ok(response.Data); 
    }

    [HttpGet]
    [Route("hello")]
    public async Task<IActionResult> Hello()
    {
        var x = await _catService.Test_Method();


        return Ok();
    }



}

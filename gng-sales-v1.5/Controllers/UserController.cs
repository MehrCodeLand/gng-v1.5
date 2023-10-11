using Data.Leyer.Models.ViewModels.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Leyer.Services.UserService;

namespace gng_sales_v1._5.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userSer;
    public UserController(IUserRepository userSer)
    {
         _userSer = userSer;
    }


    [HttpPost]
    [Route("createUser")]
    public async Task<IActionResult> CreateUser([FromBody]CreateUserVm userVm)
    {
        if(!ModelState.IsValid)
        {
            return NotFound(ModelState);
        }
        


        var response = await _userSer.CreateUser(userVm);
        if(response.ErrorCode < 0 )
            return NotFound(response.ErrorMessage);



        return Ok("DONE");
    }


    [HttpGet]
    [Route("getAllUser")]
    public async Task<IActionResult> GetAllUsers()
    {
        var result  = await _userSer.GetAllUser();
        if (result.ErrorCode < 0)
            return NotFound(result.ErrorMessage);



        return Ok(result.Data);
    }
}

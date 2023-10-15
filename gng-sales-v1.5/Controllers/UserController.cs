using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Leyer.Services.UserService;
using Services.Leyer.ViewModels.User;

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

    [HttpGet]
    [Route("GetAllUser")]
    public async Task<IActionResult> GetAllUsers()
    {
        var response  = await _userSer.GetAllUser();
        if (response.HasError)
            return NotFound(response.ErrorMessage);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody]CreateUserVm userVm)
    {
        if(!ModelState.IsValid)
        {
            return NotFound(ModelState);
        }

        var response = await _userSer.CreateUser(userVm);
        if(response.HasError )
            return NotFound(response);

        return Ok();
    }

    [HttpDelete]
    [Route("{userId}")]
    public async Task<IActionResult> DeleteUserById(int userId)
    {
        if(!ModelState.IsValid)
        {
            return NotFound(ModelState);
        }
        var response = await _userSer.DeleteUserById(userId);
        if (response.HasError)
            return NotFound(response.ErrorMessage);

        return Ok(response.Message);    
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserVm userVm )
    {
        if (!ModelState.IsValid)
            return NotFound(ModelState);
        var response = await _userSer.UpdateUser(userVm);
        if (response.HasError)
            return NotFound(response);

        return Ok(response);
    }
}

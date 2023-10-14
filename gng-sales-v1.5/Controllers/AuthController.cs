using goolrang_sales_v1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services.Leyer.ViewModels.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace gng_sales_v1._5.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private User user = new User();
    private readonly IConfiguration _conn;
    public AuthController(IConfiguration configuration)
    {
        _conn = configuration;
    }

    [HttpPost]
    [Route("registerUser")]
    public async Task<IActionResult> Register([FromBody] CreateUserVm userVm)
    {
        if (!ModelState.IsValid )
        {
            return NotFound(ModelState);
        }

        var passHash = BCrypt.Net.BCrypt.HashPassword(userVm.Password);


        return Ok();
    }

    [HttpPost]
    [Route("loginUser")]
    public async Task<IActionResult> Login([FromBody] LoginUserVm userVm)
    {
        //if(user.Email != userVm.Email)
        //{
        //    return BadRequest();
        //}

        if(!BCrypt.Net.BCrypt.Verify(userVm.Password , user.Password))
        {
            return BadRequest("wrong password");
        }

        var token = await CreateToken(user); 
        return Ok();
    }


    private async Task<string> CreateToken(User suer)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name , user.Email )
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _conn.GetSection("AppSetting:Token").Value!));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials : cred 
            );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);


        return jwt;
    } 
}

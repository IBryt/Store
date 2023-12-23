using IgorBryt.Store.BLL.Models.Auth;
using IgorBryt.Store.WebAPI.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IgorBryt.Store.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthManagerController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtConfig _jwtConfig;

    public AuthManagerController(
        UserManager<IdentityUser> userManager,
        JwtConfig jwtConfig,
        IOptionsMonitor<JwtConfig> _optionsMonitor)
    {
        _userManager = userManager;
        _jwtConfig = _optionsMonitor.CurrentValue;
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationRequestModel req)
    {
        if (!ModelState.IsValid)
        { 
            return BadRequest("Invalid request payload");
        }
        
        var emailExist = await _userManager.FindByNameAsync(req.Email);

        if (emailExist != null) 
        {
            return BadRequest("email already exist");
        }
        var newUser = new IdentityUser {
            UserName= req.Name,
            Email = req.Email
        };

        var isCreated = await _userManager.CreateAsync(newUser, req.Password);

        if (!isCreated.Succeeded)
        {
            return BadRequest(isCreated.Errors.Select(x => x.Description).ToList());
        }

        var token = GenerateJwtToken(newUser);
        return Ok(new RegistrationRequestResponseModel
        { 
            Result = true,
            Token = token
        });
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequestModel req)
    { 
        if (!ModelState.IsValid) 
        {
            return BadRequest("Invalid request payload");
        }

        var existingUser = await _userManager.FindByEmailAsync(req.Email);

        if (existingUser == null)
        {
            return BadRequest("Invalid authentication");
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(existingUser, req.Password);
        if (!isPasswordValid)
        {
            return BadRequest("Invalid authentication");
        }

        var token = GenerateJwtToken(existingUser);
        return Ok(new LoginRequestResponseModel
        {
            Token = token,
            Result = true
        });
    }

    private string GenerateJwtToken(IdentityUser user)
    { 
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(4),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512)
        };
        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = jwtTokenHandler.WriteToken(token);

        return jwtToken;
    }
}

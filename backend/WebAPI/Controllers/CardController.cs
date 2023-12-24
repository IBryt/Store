using IgorBryt.Store.BLL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IgorBryt.Store.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CardController : Controller
{
    [HttpGet]
    public async Task<ActionResult<string>> Get()
    {
        return "card";
    }
}

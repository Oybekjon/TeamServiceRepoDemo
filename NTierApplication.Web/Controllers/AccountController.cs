using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NTierApplication.Service;
using NTierApplication.Service.ViewModels;

namespace NTierApplication.Web.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public ActionResult<bool> Register( RegisterUserViewModel model )
    {
        if (model.Password == model.Email || model.Password == null || model.Email == null)
            return BadRequest(false);

       

        var res = _userService.Register( model );
        Thread.Sleep(2000);
        if( res )
            return Ok(res);

        return NotFound(res);
    }

    [HttpPost]
    public IActionResult LogIn(LogInViewModel model)
    {
        var res = _userService.Login(model);

        Thread.Sleep(2000);

        if( res.AccessToken == null )
            return NotFound();

        return Ok(res);  
    }



}

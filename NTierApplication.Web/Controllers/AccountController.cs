using Microsoft.AspNetCore.Mvc;
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
    public IActionResult Register( RegisterUserViewModel model )
    {
        model.Password 
            = BCrypt.Net.BCrypt.HashPassword( model.Password );

        var res = _userService.Register( model );

        if(res == true)
            return Ok(model);

        return BadRequest(model);
    }

    [HttpPost]
    public IActionResult LogIn(LogInViewModel model)
    {
        var res = _userService.Login(model);

        bool check = false;

        foreach(var user in res)
        {
            if(BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                check = true;
        }


        if(check)
            return Ok(model);

        return BadRequest();
    }



}

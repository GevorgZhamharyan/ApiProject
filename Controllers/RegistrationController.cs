using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;

[ApiController]
[Route("[controller]")]
public class RegistrationController : ControllerBase
{
    private readonly IUserService userService;

    public RegistrationController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpPost]
    public ActionResult RegisterUser(UserModel user)
    {
        try
        {
            var newUser = userService.Register(user);
            return Ok(newUser);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
} 
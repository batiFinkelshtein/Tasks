using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using todoList.Models;
using todoList.Interfaces;
using System;
using Microsoft.AspNetCore.Authorization;
using todoList.Services;
using todoList.Models;
using System.Security.Claims;
namespace todoList.Controllers;

[ApiController]
[Route("todo")]
[Authorize(Policy = "User")]
public class UserController : ControllerBase
{
    public UserController() { }
    [HttpPost]
    [Route("[action]")]
    public ActionResult<String> Login([FromBody] User User)
    {
        var dt = DateTime.Now;

        if (User.UserName != "Wray"
        || User.Password != $"W{dt.Year}#{dt.Day}!")
        {
            return Unauthorized();
        }

        var claims = new List<Claim>
            {
                new Claim("type", "Admin"),
            };

        var token = TokenServise.GetToken(claims);

        return new OkObjectResult(TokenServise.WriteToken(token));
    }




}
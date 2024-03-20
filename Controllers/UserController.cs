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
[Route("user")]
//[Authorize(Policy = "User")]
public class UserController : ControllerBase
{
    public UserController() { }
    [HttpPost]
    [Route("[action]")]
    public ActionResult<String> Login([FromBody] User User)
    {
        var dt = DateTime.Now;
        User user=new User();
        user=UserService.findMe(User);

        if (user==null)
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


//   [HttpGet]
//   [Authorize(Policy = "User")]
//     public ActionResult<List<task>> GetTasks()
//     {
//         return UserService.GetAllTasks();
//     }

}
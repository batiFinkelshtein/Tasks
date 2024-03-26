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
[Route("login")]
//[Authorize(Policy = "User")]
public class LoginController : ControllerBase
{
    
    public User Myuser=null;
    [HttpPost]
    [Route("[action]")]
    public ActionResult<String> Login([FromBody] User user)
    {
        
       // List<task> users=IuserService.GetAllTasks();
        //Myuser=IuserService.findMe(user);

        if (user.Password.Equals("ffff"))
        {
            return Unauthorized();
        }

        var claims = new List<Claim>
            {
                new Claim("type", "User"),
            };

        var token = TokenServise.GetToken(claims);

        return new OkObjectResult(TokenServise.WriteToken(token));
    }
    }
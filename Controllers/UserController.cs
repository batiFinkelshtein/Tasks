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
using Microsoft.Extensions.Configuration;
namespace todoList.Controllers;


[ApiController]
[Route("user")]
//[Authorize(Policy = "User")]
public class UserController : ControllerBase
{
     Iuser IuserService;
     public UserController(Iuser iuser)
    {
        this.IuserService = iuser;
    }
    [HttpGet]
    public ActionResult<List<task>> Get()
    {
        return IuserService.GetAllTasks();
    }
     User user;
    [HttpPost]
    [Route("[action]")]
    public ActionResult<String> Login([FromBody] User user)
    {
        var dt = DateTime.Now;
        
        user=IuserService.findMe(user);

        if (user==null)
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


  [HttpGet]
 // [Authorize(Policy = "User")]
    public ActionResult<List<task>> GetTasks()
    {
        return IuserService.GetAllTasks();
    }

}
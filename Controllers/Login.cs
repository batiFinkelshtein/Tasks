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
public class LoginController : ControllerBase
{
         Iuser IuserService;
      public LoginController(Iuser iuser)
    {
        this.IuserService= iuser;
    }
    public  User Myuser=null;
    [HttpPost]
    [Route("[action]")]
    public ActionResult<String> Login([FromBody] String Username,String Password)
    {

        Myuser=IuserService.findMe(Username,Password);

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        if (Myuser==null)
        {
            return Unauthorized();
        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        var claims = new List<Claim>
            {
                new Claim("id", Myuser.id.ToString()),
            };
            if(Myuser.isAdmin)
            {
                claims.Add(new Claim("type", "Admin"));
            }
            else{
                claims.Add(new Claim("type", "User"));
            }

        var token = TokenServise.GetToken(claims);

        return new OkObjectResult(TokenServise.WriteToken(token));
    }
    [HttpGet]
    
    [Authorize(Policy ="User")]
    public ActionResult<List<task>> Get()
    {
        return IuserService.GetAllTasks();
    }
   
    }
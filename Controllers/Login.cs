using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using todoList.Models;
using todoList.Interfaces;
using System;
using Microsoft.AspNetCore.Authorization;
using todoList.Services;
using System.Security.Claims;

namespace todoList.Controllers;

[ApiController]
[Route("todo")]
public class LoginController : ControllerBase
{
    public Iuser IuserService;
    // public User Myuser = null;
    public LoginController(Iuser iuser)
    {
        this.IuserService = iuser;
    }

    [HttpPost]
    [Route("[action]")]
    public ActionResult<String> Login([FromBody] User User)
    {
        if (IuserService.findMe(User) == null)
        {
            return Unauthorized();
        }

        User Myuser = IuserService.findMe(User);

        var claims = new List<Claim>{new Claim("id", Myuser.id.ToString())};
        if (Myuser.isAdmin==true)
        {
            claims.Add(new Claim("type", "Admin"));
        }
        else
        {
            claims.Add(new Claim("type", "User"));
        }

        var token = TokenServise.GetToken(claims);

        return new OkObjectResult(TokenServise.WriteToken(token));
    }

    [HttpGet]
    [Authorize]
    [Route("Get")]
    public ActionResult<List<task>> Get()
    {
        return IuserService.GetAllTasks();
    }



    // [httpGet]
    // [Authorize]
    // public ActionResult<List<task>> GetById()
    // {
    //     return IuserService.GetTasksById(Myuser.id);
    // }

}
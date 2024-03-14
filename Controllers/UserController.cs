using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using todoList.Models;
using todoList.Interfaces;
using System;
using Microsoft.AspNetCore.Authorization;

namespace todoList.Controllers;

[ApiController]
[Route("todo")]
[Authorize(Policy = "User")]

public class UserController : ControllerBase
{
    Iuser iuser;
    public UserController(Iuser user)
    {
        this.iuser = user;
    }

    [HttpGet]
    [Route("[action]")]
    public ActionResult<String> AccessPublicFiles()
    {
        return new OkObjectResult($"Public Files Accessed by ");
    }

}
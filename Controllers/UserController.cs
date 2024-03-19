using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using todoList.Models;
using todoList.Interfaces;
using System;
using Microsoft.AspNetCore.Authorization;

namespace todoList.Controllers;

[ApiController, Authorize]
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
    public IActionResult Login([FromBody] Login user)
    {
        if (user is null)
        {
            return BadRequest("Invalid user request!!!");
        }
        if (user.UserName == "Jaydeep" && user.Password == "Pass@777")
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSetting["JWT:Secret"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: ConfigurationManager.AppSetting["JWT:ValidIssuer"],
                audience: ConfigurationManager.AppSetting["JWT:ValidAudience"],
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(6),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return Ok(new JWTTokenResponse { Token = tokenString });
        }
        return Unauthorized();
    }
}
using Microsoft.AspNetCore.Mvc;
using todoList.Models;
using todoList.Interfaces;
using Microsoft.AspNetCore.Authorization;
namespace todoList.Controllers;


[ApiController]
[Route("[controller]")]

public class UserController : ControllerBase
{
    Iuser IuserService;
    public int UserId { get; set; }
    public UserController(Iuser iuser, IHttpContextAccessor httpContextAccessor)
    {
        IuserService = iuser;
        UserId = int.Parse(httpContextAccessor.HttpContext?.User?.FindFirst("id")?.Value);
    }


    [HttpGet]
    [Route("[action]")]
    [Authorize]

    public ActionResult<User?> GetUser()
    {
        return IuserService.GetMyUser(UserId);
    }







}
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
    Iadmin Iadmin;
    public int UserId { get; set; }
    public UserController(Iuser iuser, Iadmin IAdmin, IHttpContextAccessor httpContextAccessor)
    {
        IuserService = iuser;
        Iadmin = IAdmin;
        UserId = int.Parse(httpContextAccessor.HttpContext?.User?.FindFirst("id")?.Value);
    }


    [HttpGet]
    [Route("[action]")]
    [Authorize]

    public ActionResult<User?> GetUser()
    {
        return IuserService.GetMyUser(UserId);
    }
    [HttpGet]
    [Route("[action]")]
    [Authorize(Policy = "Admin")]

    public ActionResult<List<User>> GetAllUsers()
    {
        return Iadmin.getAllUsers();
    }
    [HttpPost]
    [Route("[action]")]
    [Authorize(Policy = "Admin")]

    public ActionResult<int> AddUser([FromBody]User user)
    {
        return Iadmin.AddUser(user);
    }
    [HttpDelete]
    [Route("[action]/{userId}")]
    [Authorize(Policy = "Admin")]

    public ActionResult<bool> DeleteUser(int userId)
    {
        return Iadmin.DeleteUser(userId);
    }





}
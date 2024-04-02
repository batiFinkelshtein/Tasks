using Microsoft.AspNetCore.Mvc;
using todoList.Models;
using todoList.Interfaces;
using Microsoft.AspNetCore.Authorization;
namespace todoList.Controllers;


[ApiController]
[Route("[controller]")]

public class todoController : ControllerBase
{
    Iuser IuserService;
    public int UserId { get; set; }
    public todoController(Iuser iuser, IHttpContextAccessor httpContextAccessor)
    {
        IuserService = iuser;
        UserId = int.Parse(httpContextAccessor.HttpContext?.User?.FindFirst("id")?.Value);
    }


    [HttpGet()]
    [Route("{id}")]
    [Authorize(Policy = "Admin")]

    public ActionResult<List<task>> GetMyTasksList(int id)
    {
        return IuserService.GetTasksById(id);
    }
    [HttpGet]
    [Route("[action]")]
    [Authorize]
    public ActionResult<List<task>> GetMyTasks()
    {
        return IuserService.GetTasksById(UserId);
    }
    [HttpGet]
    [Authorize(Policy = "Admin")]
    public ActionResult<List<task>> GetAllUsers()
    {
        return IuserService.GetAllTasks();
    }
    [HttpPost]
     [Route("[action]")]
    [Authorize]
    public ActionResult<int> AddTask(task newTask)
    {
        return IuserService. AddTask(UserId, newTask);
    }

}
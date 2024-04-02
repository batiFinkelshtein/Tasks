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
    [Route("[action]/{id}")]
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
     [Route("[action]")]
    [Authorize(Policy = "Admin")]
    public ActionResult<List<task>> GetAllUsers()
    {
        return IuserService.GetAllTasks();
    }
    [HttpPost]
    [Route("[action]")]
    [Authorize]
    public ActionResult<int> AddTask([FromBody]task newTask)
    {
        return IuserService.AddTask(UserId, newTask);
    }
    [HttpPut]
    [Route("[action]/{id}")]
    [Authorize]
    public ActionResult<bool> UpdateTask(int id,[FromBody] task newTask)
    {
        return IuserService.UpdateTask(UserId, id, newTask);
    }

    [HttpDelete]
    [Route("[action]/{TaskId}")]
    [Authorize]
    public ActionResult<bool> DeleteTask(int TaskId)
    {
        return IuserService.DeleteTask(UserId, TaskId);
    }
}
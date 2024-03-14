using todoList.Models;
namespace todoList.Interfaces;

public interface Iuser
{
    List<task> GetAllTasks();
    List<task> GetTaskById(int id);
    int AddTask(int id,task newtask);
 
    bool UpdateTask(int id, task newtask);
    
    bool DeleteTask(int userId,int taskId);
     User GetMyUser(int id);

}
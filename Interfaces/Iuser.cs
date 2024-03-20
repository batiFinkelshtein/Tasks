using todoList.Models;
namespace todoList.Interfaces;

public interface Iuser
{
    User? findMe(User user);
    List<task> GetAllTasks();
    List<task> GetTasksById(int id);
   task? GetTaskById(int userId,int taskId);

    int AddTask(int id,task newtask);
 
    bool UpdateTask(int id, task newtask);
    
    bool DeleteTask(int userId,int taskId);
     User GetMyUser(int id);
     

}
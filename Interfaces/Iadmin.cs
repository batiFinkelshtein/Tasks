using todoList.Models;
namespace todoList.Interfaces;

public interface Iadmin

{
    // List<task> GetAllTasks();
    //  task GetTaskById(int id);
    //  int AddTask(task newtask);
 
    // bool UpdateTask(int id, task newtask);
    
    // bool DeleteTask(int id);
    //  User GetMyUser();
  public  List<User> getAllUsers();
    public int AddUser(User user);
   public bool DeleteUser(int id);

}
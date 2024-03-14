using todoList.Models;
namespace todoList.Interfaces;

public interface ITaskService
{
   public List<task> GetAll();

    public task GetById(int id);
    
    public int Add(task newtask);
 
   public bool Update(int id, task newtask);
    
   public bool Delete(int id);
}
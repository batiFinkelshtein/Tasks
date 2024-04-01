using todoList.Models;
namespace todoList.Interfaces;

public interface Iadmin

{

    public List<User> getAllUsers();
    public int AddUser(User user);
    public bool DeleteUser(int id);

}
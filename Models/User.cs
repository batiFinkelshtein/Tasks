using System.Collections.Generic;
namespace todoList.Models;

public class User
{
    public int id { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public bool isAdmin { get; set; }
    public List<task> taskList { get; set; }
}
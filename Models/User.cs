namespace todoList.Models;
using System.Collections.Generic;
public class User
{
    public int id { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public bool isAdmin { get; set; }
    public List<task> taskList { get; set; }
}
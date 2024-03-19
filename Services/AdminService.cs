using todoList.Models;
using todoList.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using System;
using System.Text.Json;
namespace todoList.Services;
public class AdminService : Iadmin
{
    List<User> users { get; }
    private string fileName = "User.json";

    public AdminService()
    {
        this.fileName = Path.Combine(/*webHost.ContentRootPath,*/ "Data", "User.json");

        using (var jsonFile = File.OpenText(fileName))
        {
            //#pragma warning disable CS8601 
            users = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            //#pragma warning restore CS8601 
        }
    }
    private void saveToFile()
    {
        File.WriteAllText(fileName, JsonSerializer.Serialize(users));
    }

    List<User> Iadmin.getAllUsers()=>users;
   
    int Iadmin.AddUser(User user)
    {
        user.id=users.Max(u=>u.id)+1;
       users.Add(user);
       saveToFile();
       return user.id;

    }

    bool Iadmin.DeleteUser(int id)
    {
       return false;
    }
}

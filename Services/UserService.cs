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
public class UserService : Iuser
{
    List<User> users { get; }
    private string fileName = "User.json";

    public UserService()
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
    public User? findMe(String name,String Password )
    {
        for (int i = 0; i < users.Count; i++)
        {
            if(users[i].Password.Equals(Password) )
            return users[i];
        }
        return null;
    }
    public  List<task> GetAllTasks()
    {
        List<task> lt = new List<task>();
        foreach (var item in users)
        {
            foreach (var t in item.taskList)
            {
                lt.Add(t);
            }
        }
        return lt;

    }
    public  List<task> GetTasksById(int id)
    {
        List<task> lt = new List<task>();
        foreach (User user in users)
        {
            if (user.id == id)
            {
                foreach (task task in user.taskList)
                {
                    lt.Add(task);
                }
            }
        }
        return lt;
    }
    public  task? GetTaskById(int userId,int taskId)
    {
       task OneTask;
        foreach (User user in users)
        {
            if (user.id == userId)
            {
                foreach (task task in user.taskList)
                {
                    if(task.Id==taskId){
                        OneTask=task;
                        return OneTask;
                    }

                }
            }
        }
        return null;
    }
    public int AddTask(int id, task newTask)
    {
        foreach (var user in users)
        {
            if (user.id == id)
            {
                newTask.Id = user.taskList.Max(t => t.Id) + 1;
                user.taskList.Add(newTask);
            }
        }
        saveToFile();
        return newTask.Id;

    }
    public bool UpdateTask(int id, task newtask)
    {

        foreach (User user in users)
        {
            int index;
            if (user.id == id)
            {
                foreach (task task in user.taskList)
                {
                    if(task.Id==newtask.Id)
                    {
                        task.IsDone=newtask.IsDone;
                        task.name=newtask.name;
                        saveToFile();
                        return true;
                    }

                }
            }

        }
return false;
    }

    public bool DeleteTask(int userId,int taskId)
    {
        int MyIndex=0;
         foreach (User user in users)
         {
            if(user.id==userId){
            for (int i = 0; i <user.taskList.Count; i++)
            {
               if( user.taskList[i].Id==taskId)
                 MyIndex=i;
            }
            user.taskList.RemoveAt(MyIndex);
            return true;
            }
         }
         return false;
    }
    public User GetMyUser(int id) { return users.Find(u=>u.id==id); }



}


public static class UserUtils
{
    public static void AddUser(this IServiceCollection services)
    {
       
        services.AddSingleton<Iuser, UserService>();
    }
}




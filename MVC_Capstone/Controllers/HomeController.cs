using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Capstone.Models;

namespace MVC_Capstone.Controllers
{
    public class HomeController : Controller
    {
        public List<User> ListOfUsers { get; set; }
        public List<Task> ListOfTasks { get; set; }

        int taskId = 0;
        public HomeController()
        {
            this.ListOfUsers = new List<User>();
            this.ListOfTasks = new List<Task>();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(User user)
        {
            ListOfUsers.Add(user);
            return View();
        }

        public ActionResult Login()
        {
            if(Session["User"] != null)
            {
                return RedirectToAction("TaskList");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (Session["User"] == null)
            {
                foreach(User u in ListOfUsers)
                {
                    if (user.Email == u.Email && user.Password == u.Password) 
                    Session["User"] = user;
                    return RedirectToAction("TaskList");
                }
            }
            else
            {
                return View();
            }
            // This happens when 
            // check user credentials
            // if successful, go to task list and start a new user session set to user params. If failed, back to login
            return View();
        }

      
        public ActionResult TaskList()
        {

            return View();
        }
        public ActionResult AddTask()
        {
            return RedirectToAction("TaskList");
        }

        [HttpPost]
        public ActionResult AddTask(Task task)
        {
            if (task.Description != null || task.Description != ""&& task.DueDate!= null)
            {
                // need to add date validation here
                taskId++;
                task.Id = taskId;
                task.Completed = false;

                ListOfTasks.Add(task);
                return RedirectToAction("TaskList");
            }

            return View();
        }
    }
}
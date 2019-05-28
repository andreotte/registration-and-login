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
        int taskId = 0;

        public ActionResult Index()
        {
            if(Session["LoggedInUser"] == null)
            {
                Session["Message"] = "No one logged in.";
            }
            else
            {
                User user = (User)Session["LoggedInUser"];
                Session["Message"] = "Logged in: " + user.Email;
            }
            return View();
        }

        //This fires when a new user is registered.
        [HttpPost]
        public ActionResult Index(User user)
        {
            Users users;
            User currentUser = (User)Session["LoggedInUser"];

            if (Session["Users"] == null)
            {
                users = new Users();
                users.AddUser(user);
                Session["Users"] = users;
                Session["Message"] = "No one is logged in.";
            }
            else if (Session["Users"] != null && Session["LoggedInUser"] == null)
            {
                users = (Users)Session["Users"];
                users.AddUser(user);
                Session["Users"] = users; 
                Session["Message"] = "No one is logged in.";
            }
            else if(Session["Users"] != null && Session["LoggedInUser"] != null)
            {
                users = (Users)Session["Users"];
                users.AddUser(user);
                Session["Users"] = users;
                Session["Message"] = "Logged in: " + currentUser.Email;
            }

            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }

        //Is this ever firing?
        [HttpPost]
        public ActionResult Registration(User user)
        {
            return View();
        }

        public ActionResult Login()
        {
            if(Session["LoggedInUser"] != null)
            {
                User user = (User)Session["LoggedInUser"];
                Session["LoggedInMessage"] = "Logged in: " + user.Email;
                Session["LoggedInUser"] = null;
                return View();
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            Users users;
            
            if (Session["LoggedInUser"] == null && Session["Users"] != null)
            {
                users = (Users) Session["Users"];

                foreach (User u in users.ListOfUsers)
                {
                    if (user.Email == u.Email && user.Password == u.Password)
                    {
                        Session["LoggedInUser"] = user;
                        return RedirectToAction("TaskList");
                    }
                }
            }
            else
            {
                return View();
            }

            return View();
        }

        public ActionResult Logout()
        {
            Session["LoggedInUser"] = null;
            return RedirectToAction("Index");
        }
      
        public ActionResult TaskList(Task task)
        {

            User user = (User)Session["LoggedInUser"];
            if (Session["LoggedInUser"] == null)
            {
                Session["TaskListMessage"] = "No one is logged in";
                return RedirectToAction("Login");
            }
            else
            {
                Session["TaskListMessage"] = "Logged in: " + user.Email;

                Tasks tasks;

                //First add task action
                if (Session["Tasks"] == null)
                {
                    tasks = new Tasks();
                    Session["Tasks"] = tasks;
                }
                else
                {
                    tasks = (Tasks)Session["Tasks"];
                }

                if (task.Completed == true)
                {
                    foreach(Task t in tasks.ListOfTasks)
                    {
                        if(t.Description == task.Description)
                        {
                            t.Completed = !t.Completed;
                        }
                    }

                    Session["tasks"] = tasks;
                }
                return View();

            }
        }



        [HttpGet]
        public ActionResult AddTask()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTask(Task task)
        {
            Tasks tasks = (Tasks)Session["tasks"];
            User user = (User)Session["LoggedInUser"];

            if (task.Description != null || task.Description != "" && task.DueDate != null)
            {
                taskId++;
                task.Completed = false;
                task.TaskCreator = user;
                tasks.AddTask(task);
                tasks = (Tasks)Session["tasks"];

                return RedirectToAction("TaskList");
            }

            return View();
        }
    }
}
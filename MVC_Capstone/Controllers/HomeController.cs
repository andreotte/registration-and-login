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
            if(Session["User"] == null)
            {
                Session["Message"] = "No one logged in.";
            }
            else
            {
                User user = (User)Session["User"];

                Session["Message"] = "Logged in: " + user.Email;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            Users users;
            User currentUser = (User)Session["User"];

            if (Session["Users"] == null)
            {
                users = new Users();
                users.AddUser(user);
                Session["Users"] = users;
            }
            else if (Session["Users"] != null)
            {
                users = (Users)Session["Users"];
                users.AddUser(user);
                Session["Users"] = users;

                //Its breaking here when trying to rgister anothern user because the current user is now set to null
                // because ghittingh login logs the user out. need to fix this and I think everythign else should be good. 
                Session["Message"] = "Logged in: " + currentUser.Email;
            }
            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(User user)
        {
            //Users users;

            //if(Session["Users"] == null)
            //{
            //    users = new Users();
            //    users.AddUser(user);
            //    Session["Users"] = users;
            //}
            //else if(Session["Users"] != null)
            //{
            //    users = (Users)Session["Users"];
            //    users.AddUser(user);
            //    Session["Users"] = users;
            //}
            return View();
        }

        public ActionResult Login()
        {
            if(Session["User"] != null)
            {
                User user = (User)Session["User"];
                Session["LoggedInMessage"] = "Logged in: " + user.Email;
                Session["User"] = null;
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
            
            if (Session["User"] == null && Session["Users"] != null)
            {
                users = (Users) Session["Users"];

                foreach(User u in users.ListOfUsers)
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

        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Index");
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

               // ListOfTasks.Add(task);
                return RedirectToAction("TaskList");
            }

            return View();
        }
    }
}
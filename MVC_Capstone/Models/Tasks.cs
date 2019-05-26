using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Capstone.Models
{
    public class Tasks
    {
        public List<Task> ListOfTasks { get; set; }

        public Tasks()
        {
            this.ListOfTasks = new List<Task>();
        }

        public void AddTask(Task task)
        {
            ListOfTasks.Add(task);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_Capstone.Models;

namespace MVC_Capstone.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool Completed { get; set; }
        public User TaskCreator { get; set; }
    }
}
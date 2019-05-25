using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Capstone.Models
{
    public class Users
    {
        public List<User> ListOfUsers { get; set; }

        public Users()
        {
            this.ListOfUsers = new List<User>();
        }

        public void AddUser(User user)
        {
            ListOfUsers.Add(user);
        }
       
        
    }
}
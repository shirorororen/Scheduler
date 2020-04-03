using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.Data.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; } 
        public int UserTypeNo { get; set; }
        public bool Admin { get; set; }
    }
}

using Scheduler.Contract.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.Contract.Entity
{
    public class UserContract
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; } 
        public UserType UserType { get; set; }
        public bool Admin { get; set; }
    }
}

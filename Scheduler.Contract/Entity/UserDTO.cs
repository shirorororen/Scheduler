using Scheduler.Contract.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.Contract.Entity
{
    public class UserDTO : MasterContract
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public bool Admin { get; set; }
        public UserType UserType { get; set; }
        public bool Valid { get; set; }
    }
}

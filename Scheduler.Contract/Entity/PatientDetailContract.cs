using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.Contract.Entity
{
    public class PatientDetailContract
    {
        public int DetailNo { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
    }
}

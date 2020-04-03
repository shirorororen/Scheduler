using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.Contract.Entity
{
    public class UserRegistration
    {
        public UserContract user { get; set; }
        public PatientDetailContract patientDetail { get; set; }
    }
}

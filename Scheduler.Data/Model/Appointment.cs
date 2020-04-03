using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.Data.Model
{
    public class Appointment
    {
        public int AppointmentId { get; set; }

        public int DoctorId { get; set; }

        public int PatientId { get; set; }

        //public DateTime DateStart { get; set; }

        //public DateTime DateEnd { get; set; }

        public bool Attended { get; set; }
    }
}

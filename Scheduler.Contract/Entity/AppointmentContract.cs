using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Scheduler.Contract.Entity
{
    public class AppointmentContract : MasterContract
    {
        public int AppointmentId { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public bool Attended { get; set; }

        [JsonIgnore]
        public bool Available{ get; set; }
    }
}

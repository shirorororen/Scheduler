using Scheduler.Contract.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Logic.Interface
{
    public interface IUserService
    {
        public AppointmentContract GetAppointment(int appointmentId);
        public List<AppointmentContract> GetAppointments(int? doctorId = null, int? patientId = null, DateTime? dateStart = null, DateTime? dateEnd = null);
        public UserDTO GetUser(int userId);
        public AppointmentContract InsertAppointment(int doctorId, int patientId, DateTime dateStart, DateTime dateEnd);
        public UserDTO Login(string username, string password);
        public UserDTO RegisterUser(UserContract user, PatientDetailContract patient = null);
        public AppointmentContract UpdateAppoint(int appointmentId, bool attended);
    }
}

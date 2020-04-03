using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Scheduler.Contract.Entity;
using Scheduler.Contract.Enum;
using Scheduler.Data;
using Scheduler.Data.Model;
using Scheduler.Logic.AutoMapperMapping;
using Scheduler.Logic.Extension;
using Scheduler.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Scheduler.Logic.Service
{
    public class UserService :  IUserService
    {
        private readonly DbContext context;
        private readonly ILogger<UserService> logger;
        private readonly IMapper mapper;
        private readonly HttpContext httpContext;

        public UserService(DbContext context, ILogger<UserService> logger, IHttpContextAccessor contextAccessor, IMapper mapper) 
        {
            this.context = context;
            this.logger = logger;
            this.mapper = mapper;
            this.httpContext = contextAccessor.HttpContext;
        }
        public UserDTO RegisterUser(UserContract user, PatientDetailContract patient = null)
        {
            try
            {
                var userContext = mapper.Map<User>(user);
                userContext.UserTypeNo = (int)user.UserType;
                var result = context.Register(userContext);
                var patientContext = patient != null ? mapper.Map<PatientDetail>(patient) : new PatientDetail();
                if (result.UserId > 0)
                {
                    if (patient != null)
                    {
                        patientContext.UserID = result.UserId;
                        patientContext = context.RegisterPatient(patientContext);
                    }
                    else {
                        return new UserDTO { Valid = false };
                    }
                }

                var dto = mapper.MergeInto<UserDTO>(result, patientContext);
                dto.Valid = true; // AutoLogin

                return dto;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("User account not created!", ex);
            }
        }

        public UserDTO GetUser(int userId)
        {
            try
            {
                var user = context.GetUser(userId);
                var patientDetail = new PatientDetail();

                if (user.UserId > 0 && user.UserTypeNo == (int)UserType.Patient)
                    patientDetail = context.GetPatient(user.UserId);

                return mapper.MergeInto<UserDTO>(user, patientDetail);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed getting user!", ex);
            }
        }

        public UserDTO Login(string username, string password) {

            try
            {
                var user = context.GetUser(username: username, password: password);
                var patientDetail = new PatientDetail();

                if (user != null)
                {
                    if (user.UserId > 0 && user.UserTypeNo == (int)UserType.Patient)
                        patientDetail = context.GetPatient(user.UserId);
                }
                else {
                    return new UserDTO { Valid = false };
                }  

                var dto = mapper.MergeInto<UserDTO>(user, patientDetail);
                dto.Valid = true;

                return dto;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed login!", ex);
            }
        }

        public AppointmentContract GetAppointment(int appointmentId) {
            try
            {
                var appointment = context.GetAppointment(appointmentId);
                var appointmentDTO = mapper.Map<AppointmentContract>(appointment);
                if (appointment.AppointmentId > 0) {
                    var patientDetail = context.GetUser(appointment.PatientId);
                    var doctorDetail = context.GetUser(appointment.DoctorId);

                    appointmentDTO.PatientName = $"{patientDetail.FirstName} {patientDetail.LastName}";
                    appointmentDTO.DoctorName = $"{doctorDetail.FirstName} {doctorDetail.LastName}";
                }

                return appointmentDTO;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed getting appointment!", ex);
            }
        }

        public List<AppointmentContract> GetAppointments(int? doctorId = null, int? patientId = null, DateTime? dateStart = null, DateTime? dateEnd = null) {
            try
            {
                var appointments = context.GetAppointments(doctorId, patientId, dateStart, dateEnd);
                var appointmentsDTO = new List<AppointmentContract>();
                foreach (var appointment in appointments) {
                    appointmentsDTO.Add(GetAppointment(appointment.AppointmentId));
                }
                return appointmentsDTO;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed getting appointments!", ex);
            }
        }

        public AppointmentContract InsertAppointment(int doctorId, int patientId, DateTime dateStart, DateTime dateEnd) {
            try
            {
                var isExist = context.GetAppointments(doctorId, patientId, dateStart, dateEnd).ToList().Any();
                var failedModel = new AppointmentContract { Success = false };

                if (isExist) {
                    failedModel.Available = false;
                    failedModel.ErrorMessage.Add("Schedule already taken");
                    return failedModel;
                }

                var doctorDetails = context.GetUser(doctorId);
                var patientDetails = context.GetUser(patientId);

                if (doctorDetails == null || doctorDetails.UserTypeNo != (int)UserType.Doctor)
                    failedModel.ErrorMessage.Add("Appointed doctor is not valid.");

                if (patientDetails == null || patientDetails.UserTypeNo != (int)UserType.Patient)
                    failedModel.ErrorMessage.Add("Appointed patient is not valid.");

                var appointment = context.InsertAppointment(doctorId, patientId, dateStart, dateEnd);
                if (appointment.AppointmentId > 0)
                    return mapper.Map<AppointmentContract>(appointment);
                else {
                    failedModel.ErrorMessage.Add("Failed creating appointment");
                    return failedModel;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed creating appointment!", ex);
            }        
        }

        public AppointmentContract UpdateAppoint(int appointmentId, bool attended)
        {
            try
            {
                var appointment = context.UpdateAppointment(appointmentId, attended);
                var failedModel = new AppointmentContract();
                if (appointment.AppointmentId > 0 && appointment.Attended == attended)
                {
                    var dto = mapper.Map<AppointmentContract>(appointment);
                    dto.Success = true;
                    return dto;
                }
                else {
                    failedModel.ErrorMessage.Add("Failed updating appointment!");
                    return failedModel;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed updating appointment!", ex);
            }
        }
    }
}

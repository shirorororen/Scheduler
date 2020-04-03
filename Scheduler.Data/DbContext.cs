using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Scheduler.Data.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Scheduler.Data
{
    public class DbContext
    {
        private const string CONNECTION_STRING_NAME = "SCHED";
        private readonly IConfiguration configuration;
        private readonly ILogger<DbContext> logger;
        private readonly IMapper mapper;
        private readonly HttpContext httpContext;

        private string Username => httpContext.User?.Identity?.Name ?? "";

        public DbContext(IConfiguration configuration, ILogger<DbContext> logger, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            this.configuration = configuration;
            this.logger = logger;
            this.mapper = mapper;
            this.httpContext = httpContextAccessor.HttpContext;
        }


        #region Helper Methods

        /// <summary>
        /// SP call which returns table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure"></param>
        /// <param name="addParameters"></param>
        /// <param name="numberOfResultSetsToSkip"></param>
        /// <returns></returns>
        internal IEnumerable<T> List<T>(string storedProcedure, Action<SqlParameterCollection> addParameters = null, int numberOfResultSetsToSkip = 0)
        {
            try
            {
                using (var con = new SqlConnection(configuration.GetConnectionString(CONNECTION_STRING_NAME)))
                {
                    con.Open();
                    SqlCommand sp = new SqlCommand(storedProcedure, con);
                    sp.CommandType = CommandType.StoredProcedure;

                    if (addParameters != null)
                        addParameters.Invoke(sp.Parameters);

                    logger.LogDebug($"List {storedProcedure} {string.Join(",", sp.Parameters.Cast<SqlParameter>().Select(p => p.Value))}");

                    using (var dataReader = sp.ExecuteReader())
                    {
                        for (int i = 0; i < numberOfResultSetsToSkip; i++)
                            dataReader.NextResult();

                        if (!dataReader.HasRows)
                            return new T[0];
                        return mapper.Map<IDataReader, IEnumerable<T>>(dataReader); 
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Database context error");
                throw ex;
            }
        }

        /// <summary>
        /// SP call nonquery
        /// </summary>
        /// <param name="storedProcedure"></param>
        /// <param name="addParameters"></param>
        internal void Call(string storedProcedure, Action<SqlParameterCollection> addParameters = null)
        {
            try
            {
                using (var con = new SqlConnection(configuration.GetConnectionString(CONNECTION_STRING_NAME)))
                {
                    con.Open();
                    SqlCommand sp = new SqlCommand(storedProcedure, con);
                    sp.CommandType = CommandType.StoredProcedure;

                    if (addParameters != null)
                        addParameters.Invoke(sp.Parameters);

                    logger.LogDebug($"Call {storedProcedure} {string.Join(",", sp.Parameters.Cast<SqlParameter>().Select(p => p.Value))}");

                    sp.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Database context error");
                throw ex;
            }
        }

        /// <summary>
        /// Used for getting output params
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        T ConvertFromDBVal<T>(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return default(T); // returns the default value for the type
            }
            else
            {
                return (T)obj;
            }
        }
        #endregion


        #region SP Calls
        // Put all business logic in Scheduler.Logic project

        public User Register(User user)
        {
            var result = List<User>("InsertUser", p =>
            {
                p.Add("@FirstName", SqlDbType.VarChar, 20).Value = user.FirstName;
                p.Add("@LastName", SqlDbType.VarChar, 20).Value = user.LastName;
                p.Add("@Username", SqlDbType.VarChar, 20).Value = user.FirstName;
                p.Add("@Password", SqlDbType.VarChar, 20).Value = user.Password;
                p.Add("@UserTypeNo", SqlDbType.Int).Value = user.UserTypeNo;
                p.Add("@Admin", SqlDbType.Bit).Value = user.Admin;
            }).FirstOrDefault();

            return result;
        }

        public PatientDetail RegisterPatient(PatientDetail patient)
        {
            var result = List<PatientDetail>("InsertPatientDetail", p =>
            {
                p.Add("@Address", SqlDbType.VarChar, 100).Value = patient.Address;
                p.Add("@PhoneNo", SqlDbType.VarChar, 20).Value = patient.PhoneNo;
                p.Add("@Email", SqlDbType.VarChar, 30).Value = patient.Email;
                p.Add("@Age", SqlDbType.Int).Value = patient.Age;
                p.Add("@Gender", SqlDbType.VarChar, 1).Value = patient.Gender;
                p.Add("@UserId", SqlDbType.Int).Value = patient.UserID;

            }).FirstOrDefault();

            return result;
        }

        public Appointment InsertAppointment(int doctorId, int patientId, DateTime dateStart, DateTime dateEnd)
        {
            var result = List<Appointment>("InsertAppointment", p =>
            {
                p.Add("@DoctorId", SqlDbType.Int).Value = doctorId;
                p.Add("@PatientId", SqlDbType.Int).Value = patientId;
                p.Add("@DateStart", SqlDbType.DateTime).Value = dateStart;
                p.Add("@DateEnd", SqlDbType.DateTime).Value = dateEnd;

            }).FirstOrDefault();

            return result;
        }

        public Appointment UpdateAppointment(int appointmentId, bool attended)
        {
            var result = List<Appointment>("UpdateAppointment", p =>
            {
                p.Add("@AppointmentId", SqlDbType.Int).Value = appointmentId;
                p.Add("@Attended", SqlDbType.Bit).Value = attended;

            }).FirstOrDefault();
            return result;
        }

        public Appointment GetAppointment(int appointmentId) {
            var result = List<Appointment>("GetAppointment", p =>
            {
                p.Add("@AppointmentId", SqlDbType.Int).Value = appointmentId;
            }).FirstOrDefault();

            return result;
        }

        public IEnumerable<Appointment> GetAppointments(int? doctorId = null, int? patientId = null, DateTime? dateStart = null, DateTime? dateEnd = null)
        {
            var result = List<Appointment>("GetAppointments", p =>
            {
                p.Add("@DoctorId", SqlDbType.Int).Value = doctorId.Value;
                p.Add("@PatientId", SqlDbType.Int).Value = patientId.Value;
                p.Add("@DateStart", SqlDbType.DateTime).Value = dateStart.Value;
                p.Add("@DateEnd", SqlDbType.DateTime).Value = dateEnd.Value;
            });

            return result;
        }

        public User GetUser(int? userId = null, string username = "", string password = "")
        {
            var result = List<User>("GetUser", p =>
            {
                p.Add("@UserId", SqlDbType.Int).Value = userId;
                p.Add("@Username", SqlDbType.VarChar, 20).Value = username;
                p.Add("@Password", SqlDbType.NVarChar, 20).Value = password;

            }).FirstOrDefault();

            return result;
        }

        public PatientDetail GetPatient(int userId)
        {
            var result = List<PatientDetail>("GetPatientDetail", p =>
            {
                p.Add("@UserId", SqlDbType.Int).Value = userId;
            }).FirstOrDefault();

            return result;
        }

        #endregion
    }
}

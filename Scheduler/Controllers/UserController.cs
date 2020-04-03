using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Scheduler.Contract.Entity;
using Scheduler.Logic.Interface;

namespace Scheduler.API.Controllers
{
    [Route("api/scheduler")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ILogger<UserController> logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            this.userService = userService;
            this.logger = logger;
        }

        /// <summary>
        /// Register User account
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("register")]  
        public ActionResult RegisterUser([FromBody] UserRegistration user) {
            try
            {
                var result = userService.RegisterUser(user.user, user.patientDetail);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "");
                throw;
            }
        }

        /// <summary>
        /// Create Appointment
        /// </summary>
        /// <param name="dateFilter"></param>
        /// <param name="doctorId"></param>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [HttpPost("appoint/{doctorId}/{patientId}")]
        public ActionResult CreateAppointment([FromBody] DateBody dateFilter, int doctorId, int patientId)
        {
            try
            {
                var result = userService.InsertAppointment(doctorId, patientId, dateFilter.DateStart, dateFilter.DateEnd);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "");
                throw;
            }
        }

        /// <summary>
        /// Update Appointment
        /// </summary>
        /// <param name="id"></param>
        /// <param name="attended"></param>
        /// <returns></returns>
        [HttpPut("appoint/{id}/{attended}/update")]
        public ActionResult UpdateAppointment(int id, bool attended) {
            try
            {
                var result = userService.UpdateAppoint(id, attended);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "");
                throw;
            }
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet("login/{username}/{password}")]
        public ActionResult Login(string username, string password) {
            try
            {
                var result = userService.Login(username, password);

                if (!result.Valid)
                    return Unauthorized();
                else
                    return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "");
                throw;
            }
        }

        /// <summary>
        /// Get Appointments
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="patientId"></param>
        /// <param name="dateFilter"></param>
        /// <returns></returns>
        [HttpPost("appointments/{doctorId}/{patientId}")]
        public ActionResult GetAppointments(int doctorId, int patientId, [FromBody] DateBody dateFilter = null) {
            try
            {
                var result = userService.GetAppointments(doctorId, patientId, dateFilter?.DateStart, dateFilter?.DateStart);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "");
                throw;
            }
        }
    }
}
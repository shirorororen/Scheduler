using AutoMapper;
using Scheduler.Contract.Entity;
using Scheduler.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.Logic.AutoMapperMapping
{
    public class LogicMapperProfile : Profile
    {
        public LogicMapperProfile()
        {
            CreateMap<UserContract, User>().ReverseMap();
            CreateMap<PatientDetailContract, PatientDetail>().ReverseMap();
            CreateMap<UserDTO, PatientDetail>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<AppointmentContract, Appointment>().ReverseMap();

        }
    }
}

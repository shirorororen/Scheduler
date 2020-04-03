using AutoMapper;
using Scheduler.Data.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Scheduler.Data.AutoMapperMapping
{
    public class DataMapperProfile : Profile
    {
        public DataMapperProfile()
        {
            CreateMap<IDataReader, User>();
            CreateMap<IDataReader, PatientDetail>();
            CreateMap<IDataReader, Appointment>();
        }
    }
}

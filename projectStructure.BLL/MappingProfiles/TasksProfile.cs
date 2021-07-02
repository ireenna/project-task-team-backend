using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using projectStructure.BLL.Models;
using projectStructure.Common.DTO;
using projectStructure.Common.DTOapp.Create;
using projectStructure.DAL;
using projectStructure.DAL.DAL;

namespace projectStructure.BLL.MappingProfiles
{
    public sealed class TasksProfile : Profile
    {
        public TasksProfile()
        {
            CreateMap<TasksDTO, Tasks>();
            CreateMap<Tasks, TasksDAL>();
            CreateMap<TasksDTO, TasksDAL>();
            CreateMap<FullTasksDAL, Tasks>()
                .ForMember(dest => dest.PerformerId, s => s.MapFrom(x => x.Performer.Id));
            CreateMap<TasksCreateDTO, TasksDAL>()
                .ForMember(dest => dest.CreatedAt, s => s.MapFrom(x => DateTime.Now))
                .ForMember(dest => dest.State, s => s.MapFrom(x => TaskStateDAL.ToDo));
        }
    }
}

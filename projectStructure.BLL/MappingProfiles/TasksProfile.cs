using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using projectStructure.Common.DTOapp.Create;
using projectStructure.DAL;

namespace projectStructure.BLL.MappingProfiles
{
    public sealed class TasksProfile : Profile
    {
        public TasksProfile()
        {
            CreateMap<TasksCreateDTO, Tasks>()
                .ForMember(dest => dest.CreatedAt, s => s.MapFrom(x => DateTime.Now))
                .ForMember(dest => dest.State, s => s.MapFrom(x => TaskState.ToDo));
        }
    }
}

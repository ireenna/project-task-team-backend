using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using projectStructure.Common;
using projectStructure.Common.DTOapp;
using projectStructure.DAL;

namespace projectStructure.BLL.MappingProfiles
{
    public sealed class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectCreateDTO, Project>()
                .ForMember(dest => dest.CreatedAt, s => s.MapFrom(x => DateTime.Now));
        }
    }
}
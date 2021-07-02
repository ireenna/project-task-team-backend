using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using projectStructure.BLL.Models;
using projectStructure.Common;
using projectStructure.Common.DTOapp;
using projectStructure.DAL;
using projectStructure.DAL.DAL;

namespace projectStructure.BLL.MappingProfiles
{
    public sealed class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectDTO, Project>();
            CreateMap<Project, ProjectDAL>();
            CreateMap<ProjectDTO, ProjectDAL>();
            CreateMap<FullProjectsDAL, Project>()
                .ForMember(dest => dest.AuthorId, s => s.MapFrom(x => x.Author.Id))
                .ForMember(dest => dest.TeamId, s => s.MapFrom(x => x.Team.Id));
            CreateMap<ProjectCreateDTO, ProjectDAL>()
                .ForMember(dest => dest.CreatedAt, s => s.MapFrom(x => DateTime.Now));
        }
    }
}
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
    public sealed class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<TeamDTO, Team>();
            CreateMap<Team, TeamDAL>();
            CreateMap<TeamDTO, TeamDAL>();
            CreateMap<Team, FullTeamDAL>()
                .ForMember(dest => dest.Id, s => s.MapFrom(x => x.Id))
                .ForMember(dest => dest.CreatedAt, s => s.MapFrom(x => x.CreatedAt))
                .ForMember(dest => dest.Name, s => s.MapFrom(x => x.Name))
                .ForMember(dest=>dest.Participants, s=> new List<FullTeamDAL>());
            CreateMap<TeamCreateDTO, TeamDAL>()
                .ForMember(dest => dest.CreatedAt, s => s.MapFrom(x => DateTime.Now));
            //CreateMap<ProjectCreateDTO, Project>()
            //    .ForMember(dest => dest.Id, s => s.MapFrom(x => x.Id))
            //    .ForMember(dest => dest.FirstName, s => s.MapFrom(x => x.LastName))
            //    .ForMember(dest => dest.LastName, s => s.MapFrom(x => x.LastName))
            //    .ForMember(dest => dest.BirthDay, s => s.MapFrom(x => x.BirthDay))
            //    .ForMember(dest => dest.Email, s => s.MapFrom(x => x.Email))
            //    .ForMember(dest => dest.RegisteredAt, s => s.MapFrom(x => x.RegisteredAt))
            //    .ForMember(dest => dest.TeamId, s => s.MapFrom(x => x.TeamId));
        }
    }
}

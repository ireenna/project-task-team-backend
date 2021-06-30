using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using projectStructure.Common.DTO;
using projectStructure.Common.Models;

namespace projectStructure.BLL.MappingProfiles
{
    public sealed class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.Id, s => s.MapFrom(x => x.Id))
                .ForMember(dest => dest.FirstName, s => s.MapFrom(x => x.LastName))
                .ForMember(dest => dest.LastName, s => s.MapFrom(x => x.LastName))
                .ForMember(dest => dest.BirthDay, s => s.MapFrom(x => x.BirthDay))
                .ForMember(dest => dest.Email, s => s.MapFrom(x => x.Email))
                .ForMember(dest => dest.RegisteredAt, s => s.MapFrom(x => x.RegisteredAt))
                .ForMember(dest => dest.TeamId, s => s.MapFrom(x => x.TeamId));

            //CreateMap<ProjectDAL, Project>()
            //    .ForMember(dest => dest.Id, s => s.MapFrom(x => x.Id))
            //    .ForMember(dest => dest.Author, s => s.MapFrom(x => x.Author))
            //    .ForMember(dest => dest.CreatedAt, s => s.MapFrom(x => x.CreatedAt))
            //    .ForMember(dest => dest.Deadline, s => s.MapFrom(x => x.Deadline))
            //    .ForMember(dest => dest.Description, s => s.MapFrom(x => x.Description))
            //    .ForMember(dest => dest.Name, s => s.MapFrom(x => x.Name))
            //    .ForMember(dest => dest.Tasks, s => s.MapFrom(x => x.Tasks))
            //    .ForMember(dest => dest.Team, s => s.MapFrom(x => x.Tasks));

            //CreateMap<UserRegisterDTO, User>()
            //    .ForMember(dest => dest.Avatar, src => src.MapFrom(s => string.IsNullOrEmpty(s.Avatar) ? null : new Image { URL = s.Avatar }));
        }
    }
}

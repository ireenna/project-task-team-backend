using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using projectStructure.Common.DTO;
using projectStructure.DAL.Models;

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

            //CreateMap<UserRegisterDTO, User>()
            //    .ForMember(dest => dest.Avatar, src => src.MapFrom(s => string.IsNullOrEmpty(s.Avatar) ? null : new Image { URL = s.Avatar }));
        }
    }
}

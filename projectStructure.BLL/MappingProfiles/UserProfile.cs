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
                .ForMember(dest => dest, s => s.MapFrom(x => x));

            //CreateMap<UserRegisterDTO, User>()
            //    .ForMember(dest => dest.Avatar, src => src.MapFrom(s => string.IsNullOrEmpty(s.Avatar) ? null : new Image { URL = s.Avatar }));
        }
    }
}

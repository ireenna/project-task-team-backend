using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using projectStructure.BLL.Models;
using projectStructure.Common.DTO;
using projectStructure.DAL;

namespace projectStructure.BLL.MappingProfiles
{
    public sealed class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDAL>();
            CreateMap<UserDAL, User>();
            CreateMap<UserDTO, UserDAL>();

        }
    }
}

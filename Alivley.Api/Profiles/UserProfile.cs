using Alively.Core.Entities;
using Alivley.Api.DTOs;
using AutoMapper;

namespace Alivley.Api.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTO, User>();

            CreateMap<User, UserDTO>();
        }
    }
}

using AutoMapper;
using Farma.DTO;
using Farma.Entities;

namespace Farma.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UsersEntity, UserDTO>();
            CreateMap<UserCreateDTO, UsersEntity>();
            CreateMap<UserUpdateDTO, UsersEntity>();
            CreateMap<UsersEntity, UsersEntity>();
        }
    }
}

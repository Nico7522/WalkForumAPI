
using AutoMapper;
using WalkForum.Application.Users.Commands.Register;
using WalkForum.Application.Users.Commands.UpdateUser;
using WalkForum.Domain.Entities;

namespace WalkForum.Application.Users.Dtos;

public class UsersProfile : Profile
{
    public UsersProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<RegisterCommand, User>();
        CreateMap<UpdateUserCommand, User>();



    }
}

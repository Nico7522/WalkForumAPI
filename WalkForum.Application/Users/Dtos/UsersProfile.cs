
using AutoMapper;
using WalkForum.Domain.Entities;

namespace WalkForum.Application.Users.Dtos;

public class UsersProfile : Profile
{
    public UsersProfile()
    {
        CreateMap<User, UserDto>();
    }
}

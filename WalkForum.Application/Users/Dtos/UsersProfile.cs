
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
        CreateMap<RegisterCommand, User>()
            .ForMember(src => src.UserProfile, opt => opt.MapFrom(opt => new { CreationDate = DateTime.Now, UpdateDate = DateTime.Now}));
        CreateMap<UpdateUserCommand, User>();
    }
}

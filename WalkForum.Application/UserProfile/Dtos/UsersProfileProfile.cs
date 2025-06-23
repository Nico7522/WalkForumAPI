using AutoMapper;
using WalkForum.Application.UserProfile.Commands.UpdateUserProfileCommand;
using Entities = WalkForum.Domain.Entities;


namespace WalkForum.Application.UserProfile.Dtos;

public class UsersProfileProfile : Profile
{
    public UsersProfileProfile()
    {
        CreateMap<UpdateUserProfileCommand, Entities.UserProfile>()
            .ForMember(d => d.UpdateDate, opt => opt.MapFrom(opt => DateTime.Now));
    }
}

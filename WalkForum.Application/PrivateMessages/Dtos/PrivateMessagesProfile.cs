
using AutoMapper;
using WalkForum.Application.PrivateMessages.Commands.UpdatePrivateMessage;
using WalkForum.Domain.Entities;

namespace WalkForum.Application.PrivateMessages.Dtos;

public class PrivateMessagesProfile : Profile
{
    public PrivateMessagesProfile()
    {
        CreateMap<PrivateMessage, PrivateMessageDto>()
            .ForCtorParam("username", opt => opt.MapFrom(src => src.UserProfile.User.UserName));
          
        CreateMap<UpdatePrivateMessageCommand, PrivateMessage>()
            .ForMember(d => d.UpdateDate, opt => opt.MapFrom(opt => DateTime.Now));
    }
}

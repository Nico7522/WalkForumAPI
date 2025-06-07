

using AutoMapper;
using WalkForum.Application.Messages.Commands.CreateMessage;
using WalkForum.Domain.Entities;

namespace WalkForum.Application.Messages.Dtos;

public class MessagesProfile : Profile
{
    public MessagesProfile()
    {
        CreateMap<CreateMessageCommand, Message>();

        CreateMap<Message, MessageDto>()
            .ForMember(msg => msg.UserId, opt => opt.MapFrom(src => src.UserId));
            //.ForMember(msg => msg.Username, opt => opt.MapFrom(src => src.User.UserName));

    }
}

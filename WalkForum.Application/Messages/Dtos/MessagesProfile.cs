

using AutoMapper;
using WalkForum.Application.Messages.Commands.CreateMessage;
using WalkForum.Application.Messages.Commands.UpdateMessage;
using WalkForum.Domain.Entities;

namespace WalkForum.Application.Messages.Dtos;

public class MessagesProfile : Profile
{
    public MessagesProfile()
    {
        CreateMap<CreateMessageCommand, Message>();

        CreateMap<Message, MessageDto>()
            .ForMember(msg => msg.userId, opt => opt.MapFrom(src => src.UserId));

        CreateMap<UpdateMessageCommand, Message>();
    }
}

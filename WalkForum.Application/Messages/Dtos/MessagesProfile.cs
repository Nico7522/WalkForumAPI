using AutoMapper;
using WalkForum.Application.Messages.Commands.CreateMessage;
using WalkForum.Application.Messages.Commands.UpdateMessage;
using WalkForum.Domain.Entities;

namespace WalkForum.Application.Messages.Dtos;

public class MessagesProfile : Profile
{
    public MessagesProfile()
    {
        CreateMap<CreateMessageCommand, Message>()
            .ForMember(msg => msg.CreationDate, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(msg => msg.UpdateDate, opt => opt.MapFrom(src => DateTime.Now));



        CreateMap<Message, MessageDto>()
            .ForCtorParam("username", opt => opt.MapFrom(src => src.User.UserName));

        CreateMap<UpdateMessageCommand, Message>()
               .ForMember(msg => msg.UpdateDate, opt => opt.MapFrom(src => DateTime.Now));
    }
}

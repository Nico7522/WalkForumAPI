
using AutoMapper;
using WalkForum.Domain.Entities;

namespace WalkForum.Application.PrivateDiscussions.Dtos;

public class PrivateDiscussionProfile : Profile
{
    public PrivateDiscussionProfile()
    {
        CreateMap<PrivateDiscussion, PrivateDiscussionDto>()
            ;
    }
}

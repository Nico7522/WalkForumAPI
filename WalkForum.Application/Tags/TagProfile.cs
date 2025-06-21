

using AutoMapper;
using WalkForum.Application.Tags.Command.UpdateTagsForPost;
using WalkForum.Domain.Entities;

namespace WalkForum.Application.Tags;

public class TagProfile : Profile
{
    public TagProfile()
    {
        CreateMap<Tag, TagDto>();
        CreateMap<UpdateTagsForPostCommand, Tag>();

    }
}

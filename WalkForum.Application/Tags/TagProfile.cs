using AutoMapper;
using WalkForum.Domain.Entities;

namespace WalkForum.Application.Tags;

public class TagProfile : Profile
{
    public TagProfile()
    {
        CreateMap<Tag, TagDto>();
    }
}

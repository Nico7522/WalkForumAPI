

using AutoMapper;
using WalkForum.Application.Posts.Commands.CreatePost;
using WalkForum.Application.Posts.Commands.UpdatePost;
using WalkForum.Domain.Entities;

namespace WalkForum.Application.Posts.Dtos;

public class PostsProfile : Profile
{
    public PostsProfile()
    {
        CreateMap<Post, PostDto>()
            .ForMember(d => d.category, opt => opt.MapFrom(src => src.Category))
            .ForMember(d => d.author, opt => opt.MapFrom(src => src.Author))
            .ForMember(d => d.messages, opt => opt.MapFrom(src => src.Messages))
            .ForMember(d => d.tags, opt => opt.MapFrom(src => src.Tags));



        CreateMap<CreatePostCommand, Post>()
            .ForMember(d => d.CreationDate, opt => opt.MapFrom(opt => DateTime.Now))
            .ForMember(d => d.UpdateDate, opt => opt.MapFrom(opt => DateTime.Now))
            .ForMember(d => d.Tags, opt => opt.Ignore());


        CreateMap<UpdatePostCommand, Post>()
            .ForMember(d => d.UpdateDate, opt => opt.MapFrom(opt => DateTime.Now));

    }
}



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
            .ForMember(d => d.Category, opt => opt.MapFrom(src => src.Category))
            .ForMember(d => d.Author, opt => opt.MapFrom(src => src.Author))
            .ForMember(d => d.Messages, opt => opt.MapFrom(src => src.Messages));


        CreateMap<CreatePostCommand, Post>();


        CreateMap<UpdatePostCommand, Post>();

    }
}

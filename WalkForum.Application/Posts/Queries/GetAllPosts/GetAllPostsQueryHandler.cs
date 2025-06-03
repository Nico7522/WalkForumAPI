

using AutoMapper;
using FluentValidation;
using MediatR;
using WalkForum.Application.Posts.Dtos;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.Posts.Queries.GetAllPosts;

internal class GetAllPostsQueryHandler(IPostsRepository postsRepository, IMapper mapper, IValidator<GetAllPostsQuery> validator) : IRequestHandler<GetAllPostsQuery, IEnumerable<PostDto>>
{
    public async Task<IEnumerable<PostDto>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
    {
        var validation = await validator.ValidateAsync(request);
        if(!validation.IsValid)
        {
            throw new Exception(validation.Errors.First().ErrorMessage);
        }
        var posts = await postsRepository.GetAll(request.Category);
        var postsDto = mapper.Map<IEnumerable<PostDto>>(posts);
        return postsDto;
    }
}

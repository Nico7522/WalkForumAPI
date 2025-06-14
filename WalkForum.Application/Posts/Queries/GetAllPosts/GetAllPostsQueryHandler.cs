using AutoMapper;
using FluentValidation;
using MediatR;
using WalkForum.Application.Posts.Dtos;
using WalkForum.Application.Utilities;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.Posts.Queries.GetAllPosts;

internal class GetAllPostsQueryHandler(IPostsRepository postsRepository, IMapper mapper, IValidator<GetAllPostsQuery> validator) : IRequestHandler<GetAllPostsQuery, IEnumerable<PostDto>>
{
    public async Task<IEnumerable<PostDto>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
    {
        await Helpers.ValidFormAsync(request, validator);
        var posts = await postsRepository.GetAll(request.Category);
        var postsDto = mapper.Map<IEnumerable<PostDto>>(posts);
        return postsDto;
    }
}

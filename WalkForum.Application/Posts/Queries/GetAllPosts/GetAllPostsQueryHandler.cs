using AutoMapper;
using MediatR;
using WalkForum.Application.Posts.Dtos;
using WalkForum.Domain.Exceptions;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.Posts.Queries.GetAllPosts;

internal class GetAllPostsQueryHandler(IPostsRepository postsRepository,
    ICategoryRepository categoryRepository,
    IMapper mapper 
    ) : IRequestHandler<GetAllPostsQuery, IEnumerable<PostDto>>
{
    public async Task<IEnumerable<PostDto>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
    {

        var category = await categoryRepository.GetByName(request.Category);
        if (category is null) throw new BadRequestException("Category doesn't exist");

        var posts = await postsRepository.GetAll(request.Category);
        var postsDto = mapper.Map<IEnumerable<PostDto>>(posts);
        return postsDto;
    }
}

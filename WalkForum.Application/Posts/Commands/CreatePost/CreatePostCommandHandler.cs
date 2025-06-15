
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Repositories;
using WalkForum.Application.Utilities;
using WalkForum.Application.Users;
using WalkForum.Domain.Exceptions;
namespace WalkForum.Application.Posts.Commands.CreatePost;

public class CreatePostCommandHandler(IMapper mapper, 
    IValidator<CreatePostCommand> validator, 
    IPostsRepository postsRepository, 
    ICategoryRepository categoryRepository,
    ILogger<CreatePostCommandHandler> logger,
    IUserContext userContext) : IRequestHandler<CreatePostCommand, int>
{
    public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Crating new post {@Post}", request);
        await Helpers.ValidFormAsync(request, validator);

        var category = await categoryRepository.GetById(request.CategoryId);
        if (category is null) throw new NotFoundException("Category not found");


        var post = mapper.Map<Post>(request);

        post.CreationDate = DateTime.Now;
        post.UpdateDate = DateTime.Now;
        post.AuthorId = userContext.GetCurrentUser().Id;

        int postId = await postsRepository.Create(post);
        return postId;
    }
}

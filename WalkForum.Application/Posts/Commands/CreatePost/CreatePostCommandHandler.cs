
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Repositories;
using WalkForum.Application.Utilities;
namespace WalkForum.Application.Posts.Commands.CreatePost;

public class CreatePostCommandHandler(IMapper mapper, IValidator<CreatePostCommand> validator, IPostsRepository postsRepository, ILogger<CreatePostCommandHandler> logger) : IRequestHandler<CreatePostCommand, int>
{
    public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Crating new post {@Post}", request);
        await Helpers.ValidFormAsync(request, validator);

        var post = mapper.Map<Post>(request);

        post.CreationDate = DateTime.Now;
        post.UpdateDate = DateTime.Now;

        int id = await postsRepository.Create(post);
        return id;
    }
}

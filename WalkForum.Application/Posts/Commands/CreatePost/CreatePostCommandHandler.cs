
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.Posts.Commands.CreatePost;

public class CreatePostCommandHandler(IMapper mapper, IValidator<CreatePostCommand> validator, IPostsRepository postsRepository, ILogger<CreatePostCommandHandler> logger) : IRequestHandler<CreatePostCommand, int>
{
    public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Crating new post {@Post}", request);
        var validation = validator.Validate(request);
        if (!validation.IsValid)
        {
            throw new Domain.Exceptions.ValidationException(validation.ToDictionary());
        }

        var post = mapper.Map<Post>(request);
        int id = await postsRepository.Create(post);
        return id;
    }
}


using AutoMapper;
using FluentValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.Posts.Commands.CreatePost;

public class CreatePostCommandHandler(IMapper mapper, IValidator<CreatePostCommand> validator, IPostsRepository postsRepository) : IRequestHandler<CreatePostCommand, int>
{
    public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
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

using AutoMapper;
using FluentValidation;
using MediatR;
using WalkForum.Domain.Exceptions;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.Posts.Commands.UpdatePost;

public class UpdatePostCommandHandler(IValidator<UpdatePostCommand> validator, IPostsRepository postsRepository, IMapper mapper) : IRequestHandler<UpdatePostCommand>
{
    public async Task Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var validation = validator.Validate(request);
        if (!validation.IsValid) {
            throw new Domain.Exceptions.ValidationException(validation.ToDictionary());
        }   
       var post = await postsRepository.GetById(request.Id);
        if (post is null) throw new NotFoundException("Post not found");

        mapper.Map(request, post);
        await postsRepository.SaveChanges();
    }
}

using AutoMapper;
using FluentValidation;
using MediatR;
using WalkForum.Application.Users;
using WalkForum.Application.Utilities;
using WalkForum.Domain.Constants;
using WalkForum.Domain.Exceptions;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.Posts.Commands.UpdatePost;

public class UpdatePostCommandHandler(IValidator<UpdatePostCommand> validator, 
    IPostsRepository postsRepository, 
    IMapper mapper,
    IUserContext userContext) : IRequestHandler<UpdatePostCommand>
{
    public async Task Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
  
        Helpers.ValidForm(request, validator);

        var post = await postsRepository.GetById(request.Id);
        if (post is null) throw new NotFoundException("Post not found");
        if (post.AuthorId != userContext.GetCurrentUser().Id && !userContext.GetCurrentUser().IsInRole(UserRoles.Administrator) && !userContext.GetCurrentUser().IsInRole(UserRoles.Moderator)) throw new ForbiddenException("Not authorized");


        mapper.Map(request, post);
        await postsRepository.SaveChanges();
    }
}

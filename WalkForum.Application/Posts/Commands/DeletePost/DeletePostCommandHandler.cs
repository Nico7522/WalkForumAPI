using MediatR;
using WalkForum.Application.Users;
using WalkForum.Domain.Constants;
using WalkForum.Domain.Exceptions;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.Posts.Commands.DeletePost;

public class DeletePostCommandHandler(IPostsRepository postsRepository, IUserContext userContext) : IRequestHandler<DeletePostCommand>
{
    async Task IRequestHandler<DeletePostCommand>.Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {

        var post = await postsRepository.GetById(request.Id);
        if (post is null) throw new NotFoundException("Post not found");

        if (post.AuthorId != userContext.GetCurrentUser().Id && !userContext.GetCurrentUser().IsInRole(UserRoles.Administrator) && !userContext.GetCurrentUser().IsInRole(UserRoles.Moderator)) throw new ForbiddenException("Not authorized");

        await postsRepository.Delete(post);
    }
}

using MediatR;
using WalkForum.Domain.AuthorizationInterfaces;
using WalkForum.Domain.Constants;
using WalkForum.Domain.Exceptions;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.Posts.Commands.DeletePost;

public class DeletePostCommandHandler(
    IPostsRepository postsRepository, 
    IPostAuthorizationService postAuthorizationService) : IRequestHandler<DeletePostCommand>
{
    async Task IRequestHandler<DeletePostCommand>.Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await postsRepository.GetById(request.Id);
        if (post is null) throw new NotFoundException("Post not found");

        if(!postAuthorizationService.Authorize(post, ResourceOperation.Delete)) throw new ForbiddenException("Not authorized");

        await postsRepository.Delete(post);
    }
}

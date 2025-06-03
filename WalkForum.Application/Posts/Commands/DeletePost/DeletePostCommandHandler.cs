using MediatR;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.Posts.Commands.DeletePost;

public class DeletePostCommandHandler(IPostsRepository postsRepository) : IRequestHandler<DeletePostCommand, bool>
{
    async Task<bool> IRequestHandler<DeletePostCommand, bool>.Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await postsRepository.GetById(request.Id);

        if (post is null) return false;

        await postsRepository.Delete(post);
        return true;
    }
}

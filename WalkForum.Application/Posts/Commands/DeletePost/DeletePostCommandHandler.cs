using MediatR;
using WalkForum.Domain.Exceptions;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.Posts.Commands.DeletePost;

public class DeletePostCommandHandler(IPostsRepository postsRepository) : IRequestHandler<DeletePostCommand>
{
    async Task IRequestHandler<DeletePostCommand>.Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await postsRepository.GetById(request.Id);

        if (post is null) throw new NotFoundException("Post not found");

        await postsRepository.Delete(post);
    }
}

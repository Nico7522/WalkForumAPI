using MediatR;
using WalkForum.Application.Users;
using WalkForum.Domain.Constants;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Exceptions;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.Tags.Command.UpdateTagsForPost;

internal class UpdateTagsForPostCommandHandler(ITagsRepository tagsRepository,
    IPostsRepository postsRepository,
    IUserContext userContext) : IRequestHandler<UpdateTagsForPostCommand>
{
    public async Task Handle(UpdateTagsForPostCommand request, CancellationToken cancellationToken)
    {
        var post = await postsRepository.GetById(request.PostId);
        if (post is null) throw new NotFoundException("Post not found");

        if(userContext.GetCurrentUser().Id != post.AuthorId && !userContext.GetCurrentUser().IsInRole(UserRoles.Administrator) && !userContext.GetCurrentUser().IsInRole(UserRoles.Moderator)) throw new UnauthorizedException("Not authorized");

        var tags = new List<Tag>();
        foreach (var tagId in request.Tags)
        {
            var tag = await tagsRepository.GetById(tagId);
            if (tag is null) throw new NotFoundException("A tag has not been found");

            tags.Add(tag);
        }

        post.Tags = tags;
        
        await postsRepository.SaveChanges();
    }
}

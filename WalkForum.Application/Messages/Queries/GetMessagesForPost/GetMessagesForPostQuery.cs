

using MediatR;
using WalkForum.Application.Messages.Dtos;

namespace WalkForum.Application.Messages.Queries.GetMessagesForPost;

public class GetMessagesForPostQuery(int postId) : IRequest<IEnumerable<MessageDto>>
{
    public int PostId { get; set; } = postId;
}

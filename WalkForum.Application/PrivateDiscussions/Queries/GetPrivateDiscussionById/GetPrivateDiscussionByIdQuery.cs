
using MediatR;
using WalkForum.Application.PrivateDiscussions.Dtos;

namespace WalkForum.Application.PrivateDiscussions.Queries.GetPrivateDiscussionById
{
    public class GetPrivateDiscussionByIdQuery(int id) : IRequest<PrivateDiscussionDto>
    {
        public int Id { get; } = id;
    }
}

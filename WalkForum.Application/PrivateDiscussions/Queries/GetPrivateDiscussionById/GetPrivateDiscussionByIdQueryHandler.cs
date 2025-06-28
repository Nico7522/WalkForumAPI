using AutoMapper;
using MediatR;
using WalkForum.Application.PrivateDiscussions.Dtos;
using WalkForum.Domain.Exceptions;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.PrivateDiscussions.Queries.GetPrivateDiscussionById;

internal class GetPrivateDiscussionByIdQueryHandler(
    IPrivateDiscussionRepository privateDiscussionRepository,
    IMapper mapper
    ) : IRequestHandler<GetPrivateDiscussionByIdQuery, PrivateDiscussionDto>
{
    public async Task<PrivateDiscussionDto> Handle(GetPrivateDiscussionByIdQuery request, CancellationToken cancellationToken)
    {
        var privateDiscussion = await privateDiscussionRepository.GetById(request.Id);
        if (privateDiscussion is null) throw new NotFoundException("Private discussion not found");

        return mapper.Map<PrivateDiscussionDto>(privateDiscussion);
    }
}

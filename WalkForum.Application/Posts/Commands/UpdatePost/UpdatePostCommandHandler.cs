using AutoMapper;
using FluentValidation;
using MediatR;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.Posts.Commands.UpdatePost;

public class UpdatePostCommandHandler(IValidator<UpdatePostCommand> validator, IPostsRepository postsRepository, IMapper mapper) : IRequestHandler<UpdatePostCommand, bool>
{
    public async Task<bool> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {

        var validation = validator.Validate(request);
        if (!validation.IsValid) {
            throw new Domain.Exceptions.ValidationException(validation.ToDictionary());
        }   
       var post = await postsRepository.GetById(request.Id);
        if (post is null) return false;


        mapper.Map(request, post);


        await postsRepository.SaveChanges();

        return true;
                ;
    }
}

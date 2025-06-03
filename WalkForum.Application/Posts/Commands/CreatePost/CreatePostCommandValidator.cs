using FluentValidation;
using WalkForum.Application.Posts.Dtos;

namespace WalkForum.Application.Posts.Commands.CreatePost;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(dto => dto.Title).Length(5, 50).WithMessage("Title's length doesn't match the requirements");
        RuleFor(dto => dto.Content).MinimumLength(2).NotEmpty().WithMessage("Content can't be empty");
        RuleFor(dto => dto.CategoryId).NotEmpty();
        RuleFor(dto => dto.AuthorId).NotEmpty();
    }
}

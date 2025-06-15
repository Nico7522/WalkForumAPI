using FluentValidation;

namespace WalkForum.Application.Posts.Commands.CreatePost;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(dto => dto.Title).Length(5, 50).WithMessage("Title's length doesn't match the requirements");
        RuleFor(dto => dto.Content).NotEmpty().WithMessage("Content is required");
        RuleFor(dto => dto.CategoryId).NotEmpty().WithMessage("CategoryId is required");
    }
}

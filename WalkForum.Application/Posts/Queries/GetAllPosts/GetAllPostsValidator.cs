

using FluentValidation;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.Posts.Queries.GetAllPosts;

public class GetAllPostsValidator : AbstractValidator<GetAllPostsQuery>
{
    ICategoryRepository _categoryRepository;
    public GetAllPostsValidator(ICategoryRepository categoryRepository)
    {
        this._categoryRepository = categoryRepository;

        RuleFor(x => x.Category).MustAsync(async (category, cancellation) =>
        {
            var isCategoryExist = await _categoryRepository.GetByName(category);
            return isCategoryExist is not null ? true : false;
        }).WithMessage("Category doesn't exist");
    }
}

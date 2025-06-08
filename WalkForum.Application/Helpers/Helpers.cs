using FluentValidation;

namespace WalkForum.Application.Helpers;

internal static class Helpers
{
    internal static void ValidForm<T>(T command, IValidator<T> validator)
    {
        var validation = validator.Validate(command);
        if (!validation.IsValid)
        {
            throw new Domain.Exceptions.ValidationException(validation.ToDictionary());
        }
    }

    internal static async Task ValidFormAsync<T>(T command, IValidator<T> validator)
    {
        var validation = await validator.ValidateAsync(command);
        if (!validation.IsValid)
        {
            throw new Domain.Exceptions.ValidationException(validation.ToDictionary());
        }
    }
}

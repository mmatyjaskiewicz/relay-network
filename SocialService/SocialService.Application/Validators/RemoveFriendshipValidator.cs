using FluentValidation;
using SocialService.Application.DTOs.Requests;

namespace SocialService.Application.Validators;

public class RemoveFriendshipValidator : AbstractValidator<RemoveFriendshipDto>
{
    public RemoveFriendshipValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.");
    }
}
using FluentValidation;
using SocialService.Application.DTOs.Requests;

namespace SocialService.Application.Validators;

public class DeclineFriendRequestValidator : AbstractValidator<DeclineFriendRequestDto>
{
    public DeclineFriendRequestValidator()
    {
        RuleFor(x => x.RequestId)
            .NotEmpty().WithMessage("RequestId is required.");
    }
}
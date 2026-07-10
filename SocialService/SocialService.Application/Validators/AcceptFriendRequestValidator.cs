using FluentValidation;
using SocialService.Application.DTOs.Requests;

namespace SocialService.Application.Validators;

public class AcceptFriendRequestValidator : AbstractValidator<AcceptFriendRequestDto>
{
    public AcceptFriendRequestValidator()
    {
        RuleFor(x => x.RequestId)
            .NotEmpty().WithMessage("RequestId is required.");
    }
}
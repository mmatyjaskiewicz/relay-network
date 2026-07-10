using FluentValidation;
using SocialService.Application.DTOs.Requests;

namespace SocialService.Application.Validators;

public class SendFriendRequestValidator : AbstractValidator<SendFriendRequestDto>
{
    public SendFriendRequestValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.");
    }
}
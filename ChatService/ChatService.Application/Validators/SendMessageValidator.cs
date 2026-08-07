using ChatService.Application.DTOs;
using FluentValidation;

namespace ChatService.Application.Validators;

public class SendMessageValidator : AbstractValidator<SendMessageRequest>
{
    public SendMessageValidator()
    {
        RuleFor(x => x.ChatId)
            .NotEmpty().WithMessage("ChatId is required.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content is required.")
            .MaximumLength(1000).WithMessage("Content must not exceed 1000 characters.");
    }
}
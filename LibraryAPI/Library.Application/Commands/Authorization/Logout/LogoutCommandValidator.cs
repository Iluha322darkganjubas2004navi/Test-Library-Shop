using FluentValidation;
using MediatR;

namespace Library.Application.Commands.Authorization.Logout;

public class LogoutCommandValidator : AbstractValidator<LogoutCommand>
{
    public LogoutCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("Refresh Token cannot be empty.");
    }
}
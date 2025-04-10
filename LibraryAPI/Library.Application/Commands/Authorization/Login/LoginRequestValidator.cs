using FluentValidation;
using Library.Application.DTOs.Authorization;
using Library.Application.Commands.Authorization.Login; // Ensure this namespace is present

namespace Library.Application.Commands.Authorization.Login;

public class LoginRequestValidator : AbstractValidator<LoginCommand>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.loginRequest.Name)
            .NotEmpty().WithMessage("Username cannot be empty.");

        RuleFor(x => x.loginRequest.Password)
            .NotEmpty().WithMessage("Password cannot be empty.");
    }
}
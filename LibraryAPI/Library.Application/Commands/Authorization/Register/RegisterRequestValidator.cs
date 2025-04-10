using FluentValidation;
using Library.Application.DTOs.Authorization;

namespace Library.Application.Commands.Authorization.Register;

public class RegisterRequestValidator : AbstractValidator<RegisterCommand>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.registerRequest.Name)
            .NotEmpty().WithMessage("Username cannot be empty.")
            .Length(4, 32).WithMessage("Username length must be between 4 and 32 characters.");

        RuleFor(x => x.registerRequest.Password)
            .NotEmpty().WithMessage("Password cannot be empty.")
            .Length(4, 32).WithMessage("Password length must be between 4 and 32 characters.")
            .Equal(x => x.registerRequest.PasswordRepeat).WithMessage("Passwords do not match.");

        RuleFor(x => x.registerRequest.PasswordRepeat)
            .NotEmpty().WithMessage("Password confirmation cannot be empty.");

        RuleFor(x => x.registerRequest.Email)
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("Invalid email format.");
    }
}
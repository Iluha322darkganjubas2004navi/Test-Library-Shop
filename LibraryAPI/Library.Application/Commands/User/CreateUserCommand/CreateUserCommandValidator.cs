
using FluentValidation;
using Library.Application.DTOs.User;

namespace Library.Application.Commands.User.CreateUserCommand
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(user => user.createUserDTO.Name)
                .NotEmpty().WithMessage("Name can't be empty.")
                .Length(2, 50).WithMessage("Name length must be between 2 and 50 characters.");

            RuleFor(user => user.createUserDTO.Email)
                .NotEmpty().WithMessage("Email can't be empty.")
                .EmailAddress().WithMessage("Incorrect email format.");

            RuleFor(user => user.createUserDTO.Password)
                .NotEmpty().WithMessage("Password can't be empty.")
                .MinimumLength(6).WithMessage("Password length must be atleast 6 characters.");
        }
    }
}

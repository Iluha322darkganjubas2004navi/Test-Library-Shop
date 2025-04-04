using FluentValidation;
using Library.Application.DTOs.User;

namespace Library.Application.Commands.User.UpdateUserCommand
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(user => user.updateUserDTO.Id)
                .NotEmpty().WithMessage("User ID can't be empty.");

            RuleFor(user => user.updateUserDTO.Name)
                .NotEmpty().WithMessage("Name can't be empty.")
                .Length(2, 50).WithMessage("Name length must be between 2 and 50 characters.");

            RuleFor(user => user.updateUserDTO.Email)
                .NotEmpty().WithMessage("Email can't be empty.")
                .EmailAddress().WithMessage("Incorrect email format.");
        }
    }
}

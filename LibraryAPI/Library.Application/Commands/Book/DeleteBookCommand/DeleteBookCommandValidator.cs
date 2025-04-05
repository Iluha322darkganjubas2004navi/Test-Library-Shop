using FluentValidation;
using System;

namespace Library.Application.Commands.Book.DeleteBookCommand;

public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Id cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("Id must be a valid GUID.");
    }
}
using FluentValidation;
using System;

namespace Library.Application.Commands.BookBorrowing.DeleteBookBorrowingCommand;

public class DeleteBookBorrowingCommandValidator : AbstractValidator<DeleteBookBorrowingCommand>
{
    public DeleteBookBorrowingCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Id cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("Id must be a valid GUID.");
    }
}
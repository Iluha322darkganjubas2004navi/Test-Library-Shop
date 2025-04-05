using FluentValidation;
using Library.Domain.DTOs;
using System;

namespace Library.Application.Commands.BookBorrowing.CreateBookBorrowingCommand;

public class CreateBookBorrowingCommandValidator : AbstractValidator<CreateBookBorrowingCommand>
{
    public CreateBookBorrowingCommandValidator()
    {
        RuleFor(borrowing => borrowing.CreateBookBorrowingDto.BookId)
            .NotEmpty().WithMessage("BookId cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("BookId must be a valid GUID.");

        RuleFor(borrowing => borrowing.CreateBookBorrowingDto.UserId)
            .NotEmpty().WithMessage("UserId cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("UserId must be a valid GUID.");

        RuleFor(borrowing => borrowing.CreateBookBorrowingDto.BorrowedDate)
            .NotEmpty().WithMessage("BorrowedDate cannot be empty.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("BorrowedDate cannot be in the future.");

        RuleFor(borrowing => borrowing.CreateBookBorrowingDto.ReturnDate)
            .NotEmpty().WithMessage("ReturnDate cannot be empty.")
            .GreaterThan(borrowing => borrowing.CreateBookBorrowingDto.BorrowedDate)
            .WithMessage("ReturnDate must be after BorrowedDate.");
    }
}
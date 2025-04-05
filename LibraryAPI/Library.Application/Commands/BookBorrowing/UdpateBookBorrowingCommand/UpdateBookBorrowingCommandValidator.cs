using FluentValidation;
using Library.Domain.DTOs;
using System;

namespace Library.Application.Commands.BookBorrowing.UpdateBookBorrowingCommand;

public class UpdateBookBorrowingCommandValidator : AbstractValidator<UpdateBookBorrowingCommand>
{
    public UpdateBookBorrowingCommandValidator()
    {
        RuleFor(borrowing => borrowing.UpdateBookBorrowingDto.Id)
            .NotEmpty().WithMessage("Id cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("Id must be a valid GUID.");

        RuleFor(borrowing => borrowing.UpdateBookBorrowingDto.BookId)
            .NotEmpty().WithMessage("BookId cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("BookId must be a valid GUID.");

        RuleFor(borrowing => borrowing.UpdateBookBorrowingDto.UserId)
            .NotEmpty().WithMessage("UserId cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("UserId must be a valid GUID.");

        RuleFor(borrowing => borrowing.UpdateBookBorrowingDto.BorrowedDate)
            .NotEmpty().WithMessage("BorrowedDate cannot be empty.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("BorrowedDate cannot be in the future.");

        RuleFor(borrowing => borrowing.UpdateBookBorrowingDto.ReturnDate)
            .NotEmpty().WithMessage("ReturnDate cannot be empty.")
            .GreaterThan(borrowing => borrowing.UpdateBookBorrowingDto.BorrowedDate)
            .WithMessage("ReturnDate must be after BorrowedDate.");

        RuleFor(borrowing => borrowing.UpdateBookBorrowingDto.ReturnedAt)
            .LessThanOrEqualTo(DateTime.Now).When(b => b.UpdateBookBorrowingDto.ReturnedAt.HasValue)
            .WithMessage("ReturnedAt cannot be in the future.");
    }
}
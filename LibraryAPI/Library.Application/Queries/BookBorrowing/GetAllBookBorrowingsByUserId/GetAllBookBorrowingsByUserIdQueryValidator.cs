using FluentValidation;
using System;

namespace Library.Application.Queries.BookBorrowing.GetAllBookBorrowingsByUserIdQuery;

public class GetAllBookBorrowingsByUserIdQueryValidator : AbstractValidator<GetAllBookBorrowingsByUserIdQuery>
{
    public GetAllBookBorrowingsByUserIdQueryValidator()
    {
        RuleFor(query => query.UserId)
            .NotEmpty().WithMessage("UserId cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("UserId must be a valid GUID.");
    }
}
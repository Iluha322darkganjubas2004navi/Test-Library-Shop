using FluentValidation;
using System;

namespace Library.Application.Queries.BookBorrowing.GetAllBookBorrowingsByBookIdQuery;

public class GetAllBookBorrowingsByBookIdQueryValidator : AbstractValidator<GetAllBookBorrowingsByBookIdQuery>
{
    public GetAllBookBorrowingsByBookIdQueryValidator()
    {
        RuleFor(query => query.BookId)
            .NotEmpty().WithMessage("BookId cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("BookId must be a valid GUID.");
    }
}
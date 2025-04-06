using FluentValidation;
using Library.Application.Queries.BookBorrowing.GetBookBorrowingByIdQuery;
using System;

namespace Library.Application.Queries.BookBorrowing.GetBookBorrowingByIdQuery; 

public class GetBookBorrowingByIdQueryValidator : AbstractValidator<GetBookBorrowingByIdQuery>
{
    public GetBookBorrowingByIdQueryValidator()
    {
        RuleFor(query => query.BorrowingId)
            .NotEmpty().WithMessage("BorrowingId cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("BorrowingId must be a valid GUID.");
    }
}
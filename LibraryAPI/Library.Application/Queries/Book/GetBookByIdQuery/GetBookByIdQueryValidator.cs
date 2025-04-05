using FluentValidation;
using System;

namespace Library.Application.Queries.Book.GetBookByIdQuery;

public class GetBookByIdQueryValidator : AbstractValidator<GetBookByIdQuery>
{
    public GetBookByIdQueryValidator()
    {
        RuleFor(query => query.BookId)
            .NotEmpty().WithMessage("Id cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("Id must be a valid GUID.");
    }
}
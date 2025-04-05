using FluentValidation;
using System;

namespace Library.Application.Queries.Book.GetBooksByAuthorIdQuery;

public class GetBooksByAuthorIdQueryValidator : AbstractValidator<GetBooksByAuthorIdQuery>
{
    public GetBooksByAuthorIdQueryValidator()
    {
        RuleFor(query => query.AuthorId)
            .NotEmpty().WithMessage("AuthorId cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("AuthorId must be a valid GUID.");
    }
}

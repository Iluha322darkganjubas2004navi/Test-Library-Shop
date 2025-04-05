using FluentValidation;
using System;

namespace Library.Application.Queries.Author.GetAuthorByIdQuery;

public class GetAuthorByIdQueryValidator : AbstractValidator<GetAuthorByIdQuery>
{
    public GetAuthorByIdQueryValidator()
    {
        RuleFor(query => query.AuthorId)
            .NotEmpty().WithMessage("AuthorId cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("AuthorId must be a valid GUID.");
    }
}
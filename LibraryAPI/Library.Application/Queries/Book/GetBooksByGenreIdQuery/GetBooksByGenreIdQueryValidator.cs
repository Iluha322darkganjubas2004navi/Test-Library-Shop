using FluentValidation;
using System;

namespace Library.Application.Queries.Book.GetBooksByGenreIdQuery;

public class GetBooksByGenreIdQueryValidator : AbstractValidator<GetBooksByGenreIdQuery>
{
    public GetBooksByGenreIdQueryValidator()
    {
        RuleFor(query => query.GenreId)
            .NotEmpty().WithMessage("GenreId cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("GenreId must be a valid GUID.");
    }
}
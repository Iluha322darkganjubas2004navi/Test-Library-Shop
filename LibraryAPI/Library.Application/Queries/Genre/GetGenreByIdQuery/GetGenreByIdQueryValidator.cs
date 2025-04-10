using FluentValidation;
using System;

namespace Library.Application.Queries.Genre.GetGenreByIdQuery;

public class GetGenreByIdQueryValidator : AbstractValidator<GetGenreByIdQuery>
{
    public GetGenreByIdQueryValidator()
    {
        RuleFor(query => query.GenreId)
            .NotEmpty().WithMessage("GenreId cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("GenreId must be a valid GUID.");
    }
}
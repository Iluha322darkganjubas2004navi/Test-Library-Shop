using FluentValidation;

namespace Library.Application.Queries.Book.GetBookByIsbnQuery;

public class GetBookByIsbnQueryValidator : AbstractValidator<GetBookByIsbnQuery>
{
    public GetBookByIsbnQueryValidator()
    {
        RuleFor(query => query.Isbn)
            .NotEmpty().WithMessage("ISBN cannot be empty.")
            .MaximumLength(20).WithMessage("ISBN cannot exceed 20 characters.");
    }
}
using FluentValidation;
using Library.Domain.DTOs;

namespace Library.Application.Commands.Book.CreateBookCommand;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(book => book.CreateBookDto.ISBN)
            .NotEmpty().WithMessage("ISBN cannot be empty.")
            .MaximumLength(20).WithMessage("ISBN cannot exceed 20 characters.");

        RuleFor(book => book.CreateBookDto.Title)
            .NotEmpty().WithMessage("Title cannot be empty.")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

        RuleFor(book => book.CreateBookDto.Description)
            .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");

        RuleFor(book => book.CreateBookDto.AuthorId)
            .NotEmpty().WithMessage("AuthorId cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("AuthorId must be a valid GUID.");

        RuleFor(book => book.CreateBookDto.GenreIds)
            .NotNull().WithMessage("GenreIds cannot be null.");
    }
}
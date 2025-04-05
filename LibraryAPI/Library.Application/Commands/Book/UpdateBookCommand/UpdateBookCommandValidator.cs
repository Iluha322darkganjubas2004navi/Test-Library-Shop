using FluentValidation;
using Library.Domain.DTOs;
using System;

namespace Library.Application.Commands.Book.UpdateBookCommand;

public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(book => book.UpdateBookDto.Id)
            .NotEmpty().WithMessage("Id cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("Id must be a valid GUID.");

        RuleFor(book => book.UpdateBookDto.ISBN)
            .NotEmpty().WithMessage("ISBN cannot be empty.")
            .MaximumLength(20).WithMessage("ISBN cannot exceed 20 characters.");

        RuleFor(book => book.UpdateBookDto.Title)
            .NotEmpty().WithMessage("Title cannot be empty.")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

        RuleFor(book => book.UpdateBookDto.Description)
            .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");

        RuleFor(book => book.UpdateBookDto.AuthorId)
            .NotEmpty().WithMessage("AuthorId cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("AuthorId must be a valid GUID.");

        RuleFor(book => book.UpdateBookDto.GenreIds)
            .NotNull().WithMessage("GenreIds cannot be null.");
    }
}
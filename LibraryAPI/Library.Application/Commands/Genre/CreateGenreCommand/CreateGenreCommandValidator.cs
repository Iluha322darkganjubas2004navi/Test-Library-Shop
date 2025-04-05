using FluentValidation;
using Library.Domain.DTOs;

namespace Library.Application.Commands.Genre.CreateGenreCommand;

public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
{
    public CreateGenreCommandValidator()
    {
        RuleFor(genre => genre.CreateGenreDto.Name)
            .NotEmpty().WithMessage("Name cannot be empty.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
    }
}
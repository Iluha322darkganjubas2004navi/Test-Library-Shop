using FluentValidation;
using Library.Domain.DTOs;
using System;

namespace Library.Application.Commands.Genre.UpdateGenreCommand;

public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
{
    public UpdateGenreCommandValidator()
    {
        RuleFor(genre => genre.UpdateGenreDto.Id)
            .NotEmpty().WithMessage("Id cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("Id must be a valid GUID.");

        RuleFor(genre => genre.UpdateGenreDto.Name)
            .NotEmpty().WithMessage("Name cannot be empty.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
    }
}
using FluentValidation;
using Library.Domain.DTOs;
using System;

namespace Library.Application.Commands.Author.UpdateAuthorCommand;

public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
    {
        RuleFor(author => author.UpdateAuthorDto.Id)
            .NotEmpty().WithMessage("Id cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("Id must be a valid GUID.");

        RuleFor(author => author.UpdateAuthorDto.FirstName)
            .NotEmpty().WithMessage("FirstName cannot be empty.")
            .MaximumLength(100).WithMessage("FirstName cannot exceed 100 characters.");

        RuleFor(author => author.UpdateAuthorDto.LastName)
            .NotEmpty().WithMessage("LastName cannot be empty.")
            .MaximumLength(100).WithMessage("LastName cannot exceed 100 characters.");

        RuleFor(author => author.UpdateAuthorDto.DateOfBirth)
            .NotEmpty().WithMessage("DateOfBirth cannot be empty.")
            .LessThan(DateTime.Now).WithMessage("DateOfBirth cannot be in the future.");

        RuleFor(author => author.UpdateAuthorDto.Country)
            .MaximumLength(100).WithMessage("Country cannot exceed 100 characters.");
    }
}
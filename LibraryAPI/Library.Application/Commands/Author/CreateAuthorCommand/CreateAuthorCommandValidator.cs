using FluentValidation;
using Library.Domain.DTOs;
using System;

namespace Library.Application.Commands.Author.CreateAuthorCommand;

public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(author => author.CreateAuthorDto.FirstName)
            .NotEmpty().WithMessage("FirstName cannot be empty.")
            .MaximumLength(100).WithMessage("FirstName cannot exceed 100 characters.");

        RuleFor(author => author.CreateAuthorDto.LastName)
            .NotEmpty().WithMessage("LastName cannot be empty.")
            .MaximumLength(100).WithMessage("LastName cannot exceed 100 characters.");

        RuleFor(author => author.CreateAuthorDto.DateOfBirth)
            .NotEmpty().WithMessage("DateOfBirth cannot be empty.")
            .LessThan(DateTime.Now).WithMessage("DateOfBirth cannot be in the future.");

        RuleFor(author => author.CreateAuthorDto.Country)
            .MaximumLength(100).WithMessage("Country cannot exceed 100 characters.");
    }
}
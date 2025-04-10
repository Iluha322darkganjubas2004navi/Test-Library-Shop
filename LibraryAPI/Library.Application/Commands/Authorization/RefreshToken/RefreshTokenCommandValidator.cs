using FluentValidation;
using Library.Application.Commands.Author.DeleteAuthorCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands.Authorization.RefreshToken;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(command => command.refreshTokenRequest.RefreshToken)
            .NotEmpty().WithMessage("RefreshToken cannot be empty.");
    }
}
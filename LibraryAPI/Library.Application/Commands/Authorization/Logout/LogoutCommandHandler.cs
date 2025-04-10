using FluentValidation;
using Library.Application.Commands.Authorization.Logout;
using Library.Infrastructure.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Commands.Authorization.Logout;

public class LogoutCommandHandler(
    IRefreshTokenRepository refreshTokenRepository,
    IValidator<LogoutCommand> validator
) : IRequestHandler<LogoutCommand>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
    private readonly IValidator<LogoutCommand> _validator = validator;

    public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var refreshToken = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken, cancellationToken);

        if (refreshToken != null && !refreshToken.IsRevoked && refreshToken.ExpiryDate > DateTime.UtcNow)
        {
            refreshToken.IsRevoked = true;
            await _refreshTokenRepository.UpdateAsync(refreshToken, cancellationToken);
        }
    }
}
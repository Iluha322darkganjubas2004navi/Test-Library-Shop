using System.Threading;
using System.Threading.Tasks;
using Library.Application.Commands.Authorization.Logout;
using Library.Infrastructure.Repositories;
using MediatR;

namespace Library.Application.Commands.Authorization.Logout;

public class LogoutCommandHandler(IRefreshTokenRepository refreshTokenRepository) : IRequestHandler<LogoutCommand>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;

    public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken);

        if (refreshToken != null && !refreshToken.IsUsed && !refreshToken.IsRevoked && refreshToken.ExpiryDate > DateTime.UtcNow)
        {
            refreshToken.IsRevoked = true;
            _refreshTokenRepository.UpdateAsync(refreshToken);
        }
    }
}
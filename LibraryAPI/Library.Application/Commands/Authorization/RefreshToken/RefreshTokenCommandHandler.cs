using Library.Application.Commands.Authorization.RefreshToken;
using Library.Application.DTOs.Authorization;
using Library.Domain.Entities;
using Library.Infrastructure.Repositories;
using Library.Infrastructure.Services;
using Library.Infrastructure.Services.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Commands.Authorization.RefreshToken;

public class RefreshTokenCommandHandler(
    IConfiguration configuration,
    IUserRepository userRepository,
    IRefreshTokenRepository refreshTokenRepository,
    ITokenService tokenService
) : IRequestHandler<RefreshTokenCommand, AuthenticationResult?>
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<AuthenticationResult?> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = await _refreshTokenRepository.GetByTokenAsync(request.refreshTokenRequest.RefreshToken);

        if (refreshToken == null || refreshToken.IsUsed || refreshToken.IsRevoked || refreshToken.ExpiryDate <= DateTime.UtcNow)
        {
            return null; // Refresh Token недействителен
        }

        var user = await _userRepository.GetByIdAsync(refreshToken.UserId);
        if (user == null)
        {
            return null;
        }

        refreshToken.IsUsed = true;
        _refreshTokenRepository.UpdateAsync(refreshToken);

        var newAccessToken = _tokenService.GenerateJwtToken(user);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        var newRefreshTokenEntity = new Library.Domain.Entities.RefreshToken
        {
            UserId = user.Id,
            Token = newRefreshToken,
            ExpiryDate = DateTime.UtcNow.AddDays(7),
            IsUsed = false,
            IsRevoked = false,
            AddedDate = DateTime.UtcNow
        };
        await _refreshTokenRepository.AddAsync(newRefreshTokenEntity);

        return new AuthenticationResult(newAccessToken, newRefreshToken);
    }
}
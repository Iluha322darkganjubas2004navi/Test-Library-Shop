using FluentValidation;
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
    ITokenService tokenService,
    IValidator<RefreshTokenCommand> validator
) : IRequestHandler<RefreshTokenCommand, AuthenticationResult?>
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IValidator<RefreshTokenCommand> _validator = validator;

    public async Task<AuthenticationResult?> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var refreshToken = await _refreshTokenRepository.GetByTokenAsync(request.refreshTokenRequest.RefreshToken, cancellationToken);

        if (refreshToken == null || refreshToken.IsRevoked || refreshToken.ExpiryDate <= DateTime.UtcNow)
        {
            return null; // Refresh Token недействителен
        }

        var user = await _userRepository.GetByIdAsync(refreshToken.UserId, cancellationToken);
        if (user == null)
        {
            return null;
        }

        refreshToken.IsUsed = true;
        refreshToken.ExpiryDate = DateTime.UtcNow.AddDays(7);
        var newAccessToken = _tokenService.GenerateJwtToken(user);
        await _refreshTokenRepository.UpdateAsync(refreshToken, cancellationToken);

        return new AuthenticationResult(newAccessToken, refreshToken.Token);
    }
}
using AutoMapper;
using FluentValidation;
using Library.Application.Commands.Authorization.Login;
using Library.Application.DTOs.Authorization;
using Library.Application.Exceptions;
using Library.Domain.Entities;
using Library.Infrastructure.Repositories;
using Library.Infrastructure.Services;
using Library.Infrastructure.Services.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Commands.Authorization.Login;

public class LoginCommandHandler(
    IConfiguration configuration,
    IUserRepository repository,
    IMapper mapper,
    IRefreshTokenRepository refreshTokenRepository,
    ITokenService tokenService,
    IValidator<LoginCommand> validator // Внедрили валидатор
) : IRequestHandler<LoginCommand, AuthenticationResult>
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IUserRepository _repository = repository;
    private readonly IMapper _mapper = mapper;
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IValidator<LoginCommand> _validator = validator; // Сохранили валидатор

    public async Task<AuthenticationResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var user = await _repository.GetUserAsync(u =>
                u.Name == request.loginRequest.Name && u.Password == _tokenService.Hash(request.loginRequest.Password), cancellationToken);

        if (user == null)
            throw new NotFoundException("User not found or invalid credentials");

        var accessToken = _tokenService.GenerateJwtToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();

        var refreshTokenEntity = new Library.Domain.Entities.RefreshToken
        {
            UserId = user.Id,
            Token = refreshToken,
            ExpiryDate = DateTime.UtcNow.AddDays(7),
            IsUsed = false,
            IsRevoked = false,
            AddedDate = DateTime.UtcNow
        };

        await _refreshTokenRepository.AddAsync(refreshTokenEntity, cancellationToken);

        return new AuthenticationResult(accessToken, refreshToken);
    }
}
using Library.Application.DTOs.Authorization;
using MediatR;

namespace Library.Application.Commands.Authorization.RefreshToken;

public sealed record class RefreshTokenCommand(RefreshTokenRequest refreshTokenRequest) : IRequest<AuthenticationResult?>;
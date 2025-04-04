using Library.Application.DTOs.Authorization;
using MediatR;

namespace Library.Application.Commands.Authorization.Login;

public sealed record class LoginCommand(LoginRequest loginRequest) : IRequest<string>;
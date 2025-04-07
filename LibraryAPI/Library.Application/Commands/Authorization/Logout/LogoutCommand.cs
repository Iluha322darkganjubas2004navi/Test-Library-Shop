using MediatR;

namespace Library.Application.Commands.Authorization.Logout;

public sealed record class LogoutCommand(string RefreshToken) : IRequest;
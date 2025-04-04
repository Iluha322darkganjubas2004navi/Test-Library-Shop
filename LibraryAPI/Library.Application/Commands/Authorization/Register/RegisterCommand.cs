using MediatR;
using Library.Application.DTOs.Authorization;

namespace Library.Application.Commands.Authorization.Register;

public sealed record class RegisterCommand(RegisterRequest registerRequest) : IRequest<bool>;
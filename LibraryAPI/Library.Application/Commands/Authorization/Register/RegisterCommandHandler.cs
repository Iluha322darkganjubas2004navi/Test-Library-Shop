using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Library.Application.Commands.Authorization.Register;
using Library.Application.Exceptions;
using Library.Domain.Enums;
using MediatR;
using Library.Application.Exceptions;
using Library.Domain.Enums;
using Library.Infrastructure.Repositories;
using Library.Infrastructure.Services.Interfaces;
using FluentValidation;

namespace Library.Application.Commands.Authorization.Register;

public class RegisterCommandHandler(
    IUserRepository repository,
    IMapper mapper,
    ITokenService tokenService,
    IValidator<RegisterCommand> validator // Внедрили валидатор
) : IRequestHandler<RegisterCommand, bool>
{
    private readonly IUserRepository _repository = repository;
    private readonly IMapper _mapper = mapper;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IValidator<RegisterCommand> _validator = validator; // Сохранили валидатор

    public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // Выполняем валидацию
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        if (await IsLoginUnique(request.registerRequest.Name, cancellationToken))
            throw new BadRequestException("There is already a user with this login in the system");

        var existingUser = await _repository.GetUserByEmailAsync(request.registerRequest.Email, cancellationToken);

        if (existingUser != null)
        {
            throw new BadRequestException("User with this email already exists");
        }

        request.registerRequest.Password = _tokenService.Hash(request.registerRequest.Password);

        var user = _mapper.Map<Domain.Entities.User>(request.registerRequest);

        user.Role = UserRole.User;

        await _repository.AddAsync(user, cancellationToken);

        return true;
    }

    private async Task<bool> IsLoginUnique(string login, CancellationToken cancellationToken)
    {
        var user = await _repository.GetUserByLoginAsync(login, cancellationToken);
        return user != null;
    }
}
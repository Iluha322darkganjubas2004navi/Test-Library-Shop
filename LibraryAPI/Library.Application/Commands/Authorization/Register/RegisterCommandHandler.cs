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

namespace Library.Application.Commands.Authorization.Register;

public class RegisterCommandHandler(IUserRepository repository, IMapper mapper) : IRequestHandler<RegisterCommand, bool>
{
    public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {

        if (request.registerRequest.Password != request.registerRequest.PasswordRepeat)
            throw new BadRequestException("Passwords do not match");

        if (await IsLoginUnique(request.registerRequest.Name))
            throw new BadRequestException("There is already a user with this login in the system");

        var existingUser = await repository.GetUserByEmailAsync(request.registerRequest.Email);

        if (existingUser != null)
        {
            throw new BadRequestException("User with this email already exists");
        }

        if (request.registerRequest.Name.Length is < 4 or > 32)
            throw new BadRequestException("Login length must be between 4 and 32 characters.");

        if (request.registerRequest.Password.Length is < 4 or > 32)
            throw new BadRequestException("Password length must be between 4 and 32 characters.");

        request.registerRequest.Password = Hash(request.registerRequest.Password);

        var user = mapper.Map<Domain.Entities.User>(request.registerRequest);

        user.Role = UserRole.User;

        await repository.AddAsync(user);

        return true;
    }

    private async Task<bool> IsLoginUnique(string login)
    {
        var user = await repository.GetUserByLoginAsync(login);

        return user != null;
    }

    private string Hash(string inputString)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(inputString));
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
                sb.Append(b.ToString("x2"));

            return sb.ToString();
        }
    }
}
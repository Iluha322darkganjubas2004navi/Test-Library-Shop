using Library.Application.DTOs.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands.User.CreateUserCommand
{
    public sealed record CreateUserCommand(CreateUser createUserDTO) : IRequest<bool>;
}

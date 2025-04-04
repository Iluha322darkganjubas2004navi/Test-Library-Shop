using Library.Application.DTOs.User;
using MediatR;
using Library.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands.User.UpdateUserCommand
{
    public sealed record UpdateUserCommand(UpdateUser updateUserDTO) : IRequest<bool>;
}

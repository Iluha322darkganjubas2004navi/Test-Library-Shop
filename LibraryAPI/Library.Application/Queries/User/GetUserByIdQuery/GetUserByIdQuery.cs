using Library.Application.DTOs.User;
using MediatR;
using Library.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Queries.User.GetUserByIdQuery
{
    public sealed record GetUserByIdQuery(Guid userId) : IRequest<UserDTO>;
}

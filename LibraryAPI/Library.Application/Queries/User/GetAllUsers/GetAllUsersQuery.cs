using Library.Application.DTOs.User;
using MediatR;
using Library.Application.DTOs.User;

namespace Library.Application.Queries.User.GetAllUsers;

public sealed record GetAllUsersQuery : IRequest<IEnumerable<UserDTO>>;
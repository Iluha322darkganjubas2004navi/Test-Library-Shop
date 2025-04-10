using AutoMapper;
using Library.Application.DTOs.User;
using MediatR;
using Library.Application.DTOs.User;
using Library.Infrastructure.Repositories;

namespace Library.Application.Queries.User.GetAllUsers;

public class GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
    : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDTO>>
{
    public async Task<IEnumerable<UserDTO>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var userList = await userRepository.GetAllAsync(cancellationToken);

        return mapper.Map<IEnumerable<UserDTO>>(userList);
    }
}
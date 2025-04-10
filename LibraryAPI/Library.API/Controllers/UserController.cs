using Library.Application.Commands.User.CreateUserCommand;
using Library.Application.Commands.User.DeleteUserCommand;
using Library.Application.Commands.User.UpdateUserCommand;
using Library.Application.DTOs.User;
using Library.Application.Queries.User.GetAllUsers;
using Library.Application.Queries.User.GetUserByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpGet("GetUserById/{userId:guid}")]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
    public async Task<ActionResult<UserDTO>> GetUserById(Guid userId, CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new GetUserByIdQuery(userId), cancellationToken));
    }

    [HttpGet("GetAllUsers")]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
    public async Task<ActionResult<UserDTO>> GetAllUsers(CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new GetAllUsersQuery(), cancellationToken));
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
    public async Task<ActionResult<bool>> CreateUser([FromBody] CreateUser request, CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new CreateUserCommand(request), cancellationToken));
    }

    [HttpPut]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<ActionResult<bool>> UpdateUser([FromBody] UpdateUser updateUserDto, CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new UpdateUserCommand(updateUserDto), cancellationToken));
    }

    [HttpDelete("{userId:guid}")]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<ActionResult<bool>> DeleteUser(Guid userId, CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new DeleteUserCommand(userId), cancellationToken));
    }
}
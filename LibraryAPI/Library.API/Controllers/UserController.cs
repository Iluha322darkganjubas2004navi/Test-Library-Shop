using Library.Application.Commands.User.CreateUserCommand;
using Library.Application.Commands.User.DeleteUserCommand;
using Library.Application.Commands.User.UpdateUserCommand;
using Library.Application.DTOs.User;
using Library.Application.Queries.User.GetAllUsers;
using Library.Application.Queries.User.GetUserByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpGet("GetUserById/{userId:guid}")]
    [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserDTO>> GetUserById(Guid userId)
    {
        return Ok(await mediator.Send(new GetUserByIdQuery(userId)));
    }

    [HttpGet("GetAllUsers")]
    [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserDTO>> GetAllUsers()
    {
        return Ok(await mediator.Send(new GetAllUsersQuery()));
    }

    [HttpPost]
    [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> CreateUser([FromBody] CreateUser request)
    {
        return Ok(await mediator.Send(new CreateUserCommand(request)));
    }

    [HttpPut]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> UpdateUser([FromBody] UpdateUser updateUserDto)
    {
        return Ok(await mediator.Send(new UpdateUserCommand(updateUserDto)));
    }

    [HttpDelete("{userId:guid}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> DeleteUser(Guid userId)
    {
        return Ok(await mediator.Send(new DeleteUserCommand(userId)));
    }
}
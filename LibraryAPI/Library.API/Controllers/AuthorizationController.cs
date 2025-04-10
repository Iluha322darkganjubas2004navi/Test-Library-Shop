using Library.Application.Commands.Authorization.Login;
using Library.Application.Commands.Authorization.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Library.Application.DTOs.Authorization;
using Library.Application.Commands.Authorization.RefreshToken;
using Library.Application.Commands.Authorization.Logout;
using Microsoft.AspNetCore.Authorization;
using System.Threading;

namespace Library.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorizationController(IMediator mediator) : ControllerBase
{
    [HttpPost("Login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var token = await mediator.Send(new LoginCommand(request), cancellationToken);
        return Ok(JsonConvert.SerializeObject(new { token = token }));
    }

    [HttpPost("Register")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        await mediator.Send(new RegisterCommand(request), cancellationToken);
        return Ok(JsonConvert.SerializeObject(new { message = "Registration successful." }));
    }


    [HttpPost("refresh-token")]
    [Authorize(Policy = "UserOnly")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new RefreshTokenCommand(request), cancellationToken);
        return Ok(JsonConvert.SerializeObject(new { token = result.AccessToken, refreshToken = result.RefreshToken }));
    }
    [HttpPost("logout")]
    [Authorize(Policy = "UserOnly")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Logout([FromBody] string refreshToken, CancellationToken cancellationToken)
    {
        await mediator.Send(new LogoutCommand(refreshToken), cancellationToken);
        return Ok(JsonConvert.SerializeObject(new { message = "Logged out successfully." }));
    }
}
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

namespace Library.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorizationController(IMediator mediator) : ControllerBase
{
    [HttpPost("Login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var token = await mediator.Send(new LoginCommand(request));

        return Ok(JsonConvert.SerializeObject(
            new
            {
                token = token,
            })
        );
    }

    [HttpPost("Register")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        await mediator.Send(new RegisterCommand(request));

        return Ok(JsonConvert.SerializeObject(
            new
            {
                message = "Registration successful.",
            })
        );
    }

    [HttpPost("refresh-token")]
    [Authorize(Policy = "UserOnly")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var result = await mediator.Send(new RefreshTokenCommand(request));

        if (result != null)
        {
            return Ok(JsonConvert.SerializeObject(
                new
                {
                    token = result.AccessToken,
                    refreshToken = result.RefreshToken
                })
            );
        }
        else
        {
            return Unauthorized(JsonConvert.SerializeObject(new { error = "Invalid or expired Refresh Token." }));
        }
    }
    [HttpPost("logout")]
    [Authorize(Policy = "UserOnly")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Logout([FromBody] string refreshToken)
    {
        if (string.IsNullOrEmpty(refreshToken))
        {
            return BadRequest(JsonConvert.SerializeObject(new { error = "Refresh Token is required." }));
        }

        await mediator.Send(new LogoutCommand(refreshToken));

        return Ok(JsonConvert.SerializeObject(new { message = "Logged out successfully." }));
    }
}
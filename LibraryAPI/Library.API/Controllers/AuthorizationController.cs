using Library.Application.Commands.Authorization.Login;
using Library.Application.Commands.Authorization.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Library.Application.DTOs.Authorization;

namespace Library.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorizationController(IMediator mediator) : ControllerBase
{
    [HttpPost("Login")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
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
}
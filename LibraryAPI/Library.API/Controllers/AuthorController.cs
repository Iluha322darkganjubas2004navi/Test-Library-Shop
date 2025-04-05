using Library.Application.Commands.Author.CreateAuthorCommand;
using Library.Application.Commands.Author.DeleteAuthorCommand;
using Library.Application.Commands.Author.UpdateAuthorCommand;
using Library.Application.DTOs;
using Library.Application.Queries.Author.GetAllAuthorsQuery;
using Library.Application.Queries.Author.GetAuthorByIdQuery;
using Library.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("GetAuthorById/{authorId:guid}")]
    [ProducesResponseType(typeof(AuthorDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AuthorDTO>> GetAuthorById(Guid authorId)
    {
        return Ok(await _mediator.Send(new GetAuthorByIdQuery(authorId)));
    }

    [HttpGet("GetAllAuthors")]
    [ProducesResponseType(typeof(IEnumerable<AuthorDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAllAuthors()
    {
        return Ok(await _mediator.Send(new GetAllAuthorsQuery()));
    }

    [HttpPost]
    [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> CreateAuthor([FromBody] CreateAuthorCommand request)
    {
        return StatusCode(StatusCodes.Status201Created, await _mediator.Send(request));
    }

    [HttpPut]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> UpdateAuthor([FromBody] UpdateAuthorCommand updateAuthorDto)
    {
        return Ok(await _mediator.Send(updateAuthorDto));
    }

    [HttpDelete("{authorId:guid}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> DeleteAuthor(Guid authorId)
    {
        return Ok(await _mediator.Send(new DeleteAuthorCommand(authorId)));
    }
}
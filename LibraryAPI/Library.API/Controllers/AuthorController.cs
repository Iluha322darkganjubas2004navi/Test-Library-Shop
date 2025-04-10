using Library.Application.Commands.Author.CreateAuthorCommand;
using Library.Application.Commands.Author.DeleteAuthorCommand;
using Library.Application.Commands.Author.UpdateAuthorCommand;
using Library.Application.DTOs;
using Library.Application.Queries.Author.GetAllAuthorsQuery;
using Library.Application.Queries.Author.GetAuthorByIdQuery;
using Library.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace Library.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("GetAuthorById/{authorId:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthorDTO), StatusCodes.Status200OK)]
    public async Task<ActionResult<AuthorDTO>> GetAuthorById(Guid authorId, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetAuthorByIdQuery(authorId), cancellationToken));
    }

    [HttpGet("GetAllAuthors")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<AuthorDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAllAuthors(CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetAllAuthorsQuery(), cancellationToken));
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
    public async Task<ActionResult<bool>> CreateAuthor([FromBody] CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        return StatusCode(StatusCodes.Status201Created, await _mediator.Send(request, cancellationToken));
    }

    [HttpPut]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<ActionResult<bool>> UpdateAuthor([FromBody] UpdateAuthorCommand updateAuthorDto, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(updateAuthorDto, cancellationToken));
    }

    [HttpDelete("{authorId:guid}")]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<ActionResult<bool>> DeleteAuthor(Guid authorId, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new DeleteAuthorCommand(authorId), cancellationToken));
    }
}
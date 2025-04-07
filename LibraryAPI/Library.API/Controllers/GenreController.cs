using Library.Application.Commands.Genre.CreateGenreCommand;
using Library.Application.Commands.Genre.DeleteGenreCommand;
using Library.Application.Commands.Genre.UpdateGenreCommand;
using Library.Application.DTOs;
using Library.Application.Queries.Genre.GetAllGenresQuery;
using Library.Application.Queries.Genre.GetGenreByIdQuery;
using Library.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers;

[ApiController]
[Route("[controller]")]
public class GenreController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("GetAllGenres")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<GenreDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<GenreDTO>>> GetAllGenres()
    {
        return Ok(await _mediator.Send(new GetAllGenresQuery()));
    }

    [HttpGet("GetGenreById/{genreId:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(GenreDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<GenreDTO>> GetGenreById(Guid genreId)
    {
        return Ok(await _mediator.Send(new GetGenreByIdQuery(genreId)));
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> CreateGenre([FromBody] CreateGenreCommand request)
    {
        return StatusCode(StatusCodes.Status201Created, await _mediator.Send(request));
    }

    [HttpPut]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> UpdateGenre([FromBody] UpdateGenreCommand updateGenreDto)
    {
        return Ok(await _mediator.Send(updateGenreDto));
    }

    [HttpDelete("{genreId:guid}")]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> DeleteGenre(Guid genreId)
    {
        return Ok(await _mediator.Send(new DeleteGenreCommand(genreId)));
    }
}
using Library.Application.Commands.BookBorrowing.CreateBookBorrowingCommand;
using Library.Application.Commands.BookBorrowing.DeleteBookBorrowingCommand;
using Library.Application.Commands.BookBorrowing.UpdateBookBorrowingCommand;
using Library.Application.DTOs;
using Library.Application.Queries.BookBorrowing.GetBookBorrowingByIdQuery;
using Library.Application.Queries.BookBorrowing.GetAllBookBorrowingsByBookIdQuery;
using Library.Application.Queries.BookBorrowing.GetAllBookBorrowingsByUserIdQuery;
using Library.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BookBorrowingController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("GetBookBorrowingById/{borrowingId:guid}")]
    [ProducesResponseType(typeof(BookBorrowingDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BookBorrowingDTO>> GetBookBorrowingById(Guid borrowingId)
    {
        return Ok(await _mediator.Send(new GetBookBorrowingByIdQuery(borrowingId)));
    }

    [HttpGet("GetBookBorrowingsByBookId/{bookId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<BookBorrowingDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<BookBorrowingDTO>>> GetAllBookBorrowingsByBookId(Guid bookId)
    {
        return Ok(await _mediator.Send(new GetAllBookBorrowingsByBookIdQuery(bookId)));
    }

    [HttpGet("GetBookBorrowingsByUserId/{userId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<BookBorrowingDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<BookBorrowingDTO>>> GetBookBorrowingsByUserId(Guid userId)
    {
        return Ok(await _mediator.Send(new GetAllBookBorrowingsByUserIdQuery(userId)));
    }

    [HttpPost]
    [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> CreateBookBorrowing([FromBody] CreateBookBorrowingCommand request)
    {
        return StatusCode(StatusCodes.Status201Created, await _mediator.Send(request));
    }

    [HttpPut]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> UpdateBookBorrowing([FromBody] UpdateBookBorrowingCommand updateBookBorrowingDto)
    {
        return Ok(await _mediator.Send(updateBookBorrowingDto));
    }

    [HttpDelete("{borrowingId:guid}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> DeleteBookBorrowing(Guid borrowingId)
    {
        return Ok(await _mediator.Send(new DeleteBookBorrowingCommand(borrowingId)));
    }
}
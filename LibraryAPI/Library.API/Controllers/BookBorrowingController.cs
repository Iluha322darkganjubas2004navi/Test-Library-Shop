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
using Microsoft.AspNetCore.Authorization;

namespace Library.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BookBorrowingController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("GetBookBorrowingById/{borrowingId:guid}")]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(typeof(BookBorrowingDTO), StatusCodes.Status200OK)]
    public async Task<ActionResult<BookBorrowingDTO>> GetBookBorrowingById(Guid borrowingId, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetBookBorrowingByIdQuery(borrowingId), cancellationToken));
    }

    [HttpGet("GetBookBorrowingsByBookId/{bookId:guid}")]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(typeof(IEnumerable<BookBorrowingDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BookBorrowingDTO>>> GetAllBookBorrowingsByBookId(Guid bookId, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetAllBookBorrowingsByBookIdQuery(bookId), cancellationToken));
    }

    [HttpGet("GetBookBorrowingsByUserId/{userId:guid}")]
    [Authorize(Policy = "UserOnly")]
    [ProducesResponseType(typeof(IEnumerable<BookBorrowingDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BookBorrowingDTO>>> GetBookBorrowingsByUserId(Guid userId, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetAllBookBorrowingsByUserIdQuery(userId), cancellationToken));
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
    public async Task<ActionResult<bool>> CreateBookBorrowing([FromBody] CreateBookBorrowingCommand request, CancellationToken cancellationToken)
    {
        return StatusCode(StatusCodes.Status201Created, await _mediator.Send(request, cancellationToken));
    }

    [HttpPut]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<ActionResult<bool>> UpdateBookBorrowing([FromBody] UpdateBookBorrowingCommand updateBookBorrowingDto, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(updateBookBorrowingDto, cancellationToken));
    }

    [HttpDelete("{borrowingId:guid}")]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<ActionResult<bool>> DeleteBookBorrowing(Guid borrowingId, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new DeleteBookBorrowingCommand(borrowingId), cancellationToken));
    }
}
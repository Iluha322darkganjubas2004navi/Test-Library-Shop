using Library.Application.Commands.Book.CreateBookCommand;
using Library.Application.Commands.Book.DeleteBookCommand;
using Library.Application.Commands.Book.UpdateBookCommand;
using Library.Application.Commands.User.CreateUserCommand;
using Library.Application.DTOs;
using Library.Application.DTOs.User;
using Library.Application.Queries.Book.GetAllBooksQuery;
using Library.Application.Queries.Book.GetBookByIdQuery;
using Library.Application.Queries.Book.GetBookByIsbnQuery;
using Library.Application.Queries.Book.GetBooksByAuthorIdQuery;
using Library.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController(IMediator mediator) : ControllerBase
{
    [HttpGet("GetAllBooks")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<BookDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedResult<BookDTO>>> GetAllBooks([FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new GetAllBooksQuery { Page = page, PageSize = pageSize }, cancellationToken);
        return Ok(result);
    }

    [HttpGet("GetBookById/{bookId:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(BookDTO), StatusCodes.Status200OK)]
    public async Task<ActionResult<BookDTO>> GetBookById(Guid bookId, CancellationToken cancellationToken = default)
    {
        return Ok(await mediator.Send(new GetBookByIdQuery(bookId), cancellationToken));
    }

    [HttpGet("GetBookByISBN/{isbn}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(BookDTO), StatusCodes.Status200OK)]
    public async Task<ActionResult<BookDTO>> GetBookByISBN(string isbn, CancellationToken cancellationToken = default)
    {
        return Ok(await mediator.Send(new GetBookByIsbnQuery(isbn), cancellationToken));
    }

    [HttpGet("GetBooksByAuthorId/{authorId:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<BookDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooksByAuthorId(Guid authorId, CancellationToken cancellationToken = default)
    {
        return Ok(await mediator.Send(new GetBooksByAuthorIdQuery(authorId), cancellationToken));
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
    public async Task<ActionResult<bool>> CreateBook([FromBody] CreateBook request, CancellationToken cancellationToken = default)
    {
        return Ok(await mediator.Send(new CreateBookCommand(request), cancellationToken));
    }

    [HttpPut]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<ActionResult<bool>> UpdateBook([FromBody] UpdateBook updateBookDto, CancellationToken cancellationToken = default)
    {
        return Ok(await mediator.Send(new UpdateBookCommand(updateBookDto), cancellationToken));
    }

    [HttpDelete("{bookId:guid}")]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<ActionResult<bool>> DeleteBook(Guid bookId, CancellationToken cancellationToken = default)
    {
        return Ok(await mediator.Send(new DeleteBookCommand(bookId), cancellationToken));
    }
}
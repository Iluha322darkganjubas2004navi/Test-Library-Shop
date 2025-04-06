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
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController(IMediator mediator) : ControllerBase
{
    [HttpGet("GetAllBooks")]
    [ProducesResponseType(typeof(IEnumerable<BookDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<BookDTO>>> GetAllBooks()
    {
        return Ok(await mediator.Send(new GetAllBooksQuery()));
    }

    [HttpGet("GetBookById/{bookId:guid}")]
    [ProducesResponseType(typeof(BookDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BookDTO>> GetBookById(Guid bookId)
    {
        return Ok(await mediator.Send(new GetBookByIdQuery(bookId)));
    }

    [HttpGet("GetBookByISBN/{isbn}")]
    [ProducesResponseType(typeof(BookDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BookDTO>> GetBookByISBN(string isbn)
    {
        return Ok(await mediator.Send(new GetBookByIsbnQuery(isbn)));
    }

    [HttpGet("GetBooksByAuthorId/{authorId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<BookDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooksByAuthorId(Guid authorId)
    {
        return Ok(await mediator.Send(new GetBooksByAuthorIdQuery(authorId)));
    }

    [HttpPost]
    [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> CreateBook([FromBody] CreateBook request)
    {
        return Ok(await mediator.Send(new CreateBookCommand(request)));
    }

    [HttpPut]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> UpdateBook([FromBody] UpdateBook updateBookDto)
    {
        return Ok(await mediator.Send(new UpdateBookCommand(updateBookDto)));
    }

    [HttpDelete("{bookId:guid}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> DeleteBook(Guid bookId)
    {
        return Ok(await mediator.Send(new DeleteBookCommand(bookId)));
    }
}
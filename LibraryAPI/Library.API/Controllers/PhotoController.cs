using Library.Application.Commands.Book.UploadPhotoCommand;
using Library.Application.Queries.Book.GetPhotoQuery;
using Microsoft.AspNetCore.Authorization;

namespace Library.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PhotoController(IMediator mediator) : ControllerBase
{
    [HttpPost("HandleFileUpload/{bookId}")]
    [Authorize(Policy = "AdminOnly")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> HandleFileUpload([FromRoute] Guid bookId, IFormFile file)
    {
        await mediator.Send(new UploadPhotoCommand(bookId, file));

        return Ok("Uploaded successfully");
    }

    [HttpGet("GetPhoto/{bookId}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(FileStreamResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPhoto([FromRoute] Guid bookId)
    {
        var photoStream = await mediator.Send(new GetPhotoQuery(bookId));

        return File(photoStream, "image/jpeg");
    }
}
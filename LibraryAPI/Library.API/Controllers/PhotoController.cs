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
    public async Task<IActionResult> HandleFileUpload([FromRoute] Guid bookId, IFormFile file, CancellationToken cancellationToken)
    {
        await mediator.Send(new UploadPhotoCommand(bookId, file), cancellationToken);

        return Ok("Uploaded successfully");
    }

    [HttpGet("GetPhoto/{bookId}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(FileStreamResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPhoto([FromRoute] Guid bookId, CancellationToken cancellationToken)
    {
        var photoStream = await mediator.Send(new GetPhotoQuery(bookId), cancellationToken);

        return File(photoStream, "image/jpeg");
    }
}
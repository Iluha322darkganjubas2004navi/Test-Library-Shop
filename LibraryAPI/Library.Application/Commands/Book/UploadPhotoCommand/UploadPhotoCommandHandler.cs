using Library.Infrastructure.Services.Interfaces;
using MediatR;

namespace Library.Application.Commands.Book.UploadPhotoCommand;

public class UploadPhotoCommandHandler : IRequestHandler<UploadPhotoCommand, bool>
{
    private readonly IFileStorage _fileStorage;

    public UploadPhotoCommandHandler(IFileStorage fileStorage)
    {
        _fileStorage = fileStorage;
    }
    public async Task<bool> Handle(UploadPhotoCommand request, CancellationToken cancellationToken)
    {
        var location = $"books/{request.bookId}/{request.bookId}.jpg";
        using (var fileStream = request.file.OpenReadStream())
        {
            return await _fileStorage.SaveFileAsync(location, fileStream, cancellationToken);
        }
    }
}
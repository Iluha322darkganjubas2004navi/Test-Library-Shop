using Library.Infrastructure.Services.Interfaces;
using MediatR;
using Library.Infrastructure.Services.Interfaces;

namespace Library.Application.Queries.Book.GetPhotoQuery;

public class GetPhotoQueryHandler : IRequestHandler<GetPhotoQuery, Stream>
{
    private readonly IFileStorage _fileStorage;

    public GetPhotoQueryHandler(IFileStorage fileStorage)
    {
        _fileStorage = fileStorage;
    }

    public async Task<Stream> Handle(GetPhotoQuery request, CancellationToken cancellationToken)
    {
        var location = $"books/{request.bookId}/{request.bookId}.jpg";
        return await _fileStorage.GetFileStreamAsync(location);
    }
}
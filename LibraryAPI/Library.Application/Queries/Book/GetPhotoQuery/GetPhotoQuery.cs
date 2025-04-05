using MediatR;

namespace Library.Application.Queries.Book.GetPhotoQuery;

public sealed record GetPhotoQuery(Guid bookId) : IRequest<Stream>;
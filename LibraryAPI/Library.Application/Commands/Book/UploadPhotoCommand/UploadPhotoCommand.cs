using MediatR;
using Microsoft.AspNetCore.Http;

namespace Library.Application.Commands.Book.UploadPhotoCommand;

public sealed record UploadPhotoCommand(Guid bookId, IFormFile file) : IRequest<bool>;
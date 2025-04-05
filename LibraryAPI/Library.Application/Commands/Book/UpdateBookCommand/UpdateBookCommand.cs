using Library.Domain.DTOs;
using MediatR;

namespace Library.Application.Commands.Book.UpdateBookCommand;

public sealed record UpdateBookCommand(UpdateBook UpdateBookDto) : IRequest<BookDTO>;
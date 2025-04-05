using Library.Domain.DTOs;
using MediatR;

namespace Library.Application.Commands.Book.CreateBookCommand;

public sealed record CreateBookCommand(CreateBook CreateBookDto) : IRequest<BookDTO>;
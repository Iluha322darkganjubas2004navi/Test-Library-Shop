using Library.Domain.DTOs;
using MediatR;

namespace Library.Application.Commands.BookBorrowing.CreateBookBorrowingCommand;

public sealed record CreateBookBorrowingCommand(CreateBookBorrowing CreateBookBorrowingDto) : IRequest<BookBorrowingDTO>;
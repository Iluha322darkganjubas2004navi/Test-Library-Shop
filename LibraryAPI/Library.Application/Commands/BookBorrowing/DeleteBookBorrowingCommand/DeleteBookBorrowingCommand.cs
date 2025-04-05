using MediatR;
using System;

namespace Library.Application.Commands.BookBorrowing.DeleteBookBorrowingCommand;

public sealed record DeleteBookBorrowingCommand(Guid Id) : IRequest<bool>;
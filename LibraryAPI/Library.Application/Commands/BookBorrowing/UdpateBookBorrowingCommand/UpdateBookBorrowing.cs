using Library.Domain.DTOs;
using MediatR;
using System;

namespace Library.Application.Commands.BookBorrowing.UpdateBookBorrowingCommand;

public sealed record UpdateBookBorrowingCommand(UpdateBookBorrowing UpdateBookBorrowingDto) : IRequest<BookBorrowingDTO>;
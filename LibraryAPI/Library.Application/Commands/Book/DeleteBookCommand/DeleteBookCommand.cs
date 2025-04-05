using MediatR;
using System;

namespace Library.Application.Commands.Book.DeleteBookCommand;

public sealed record DeleteBookCommand(Guid Id) : IRequest<bool>;
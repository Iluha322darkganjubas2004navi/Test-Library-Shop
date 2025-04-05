using MediatR;
using System;

namespace Library.Application.Commands.Author.DeleteAuthorCommand;

public sealed record DeleteAuthorCommand(Guid Id) : IRequest<bool>;
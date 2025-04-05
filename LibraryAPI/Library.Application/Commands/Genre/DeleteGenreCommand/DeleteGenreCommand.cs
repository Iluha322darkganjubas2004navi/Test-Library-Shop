using MediatR;
using System;

namespace Library.Application.Commands.Genre.DeleteGenreCommand;

public sealed record DeleteGenreCommand(Guid Id) : IRequest<bool>;
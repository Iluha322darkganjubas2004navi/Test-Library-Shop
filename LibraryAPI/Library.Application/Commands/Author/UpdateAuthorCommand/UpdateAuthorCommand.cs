using Library.Domain.DTOs;
using MediatR;
using System;

namespace Library.Application.Commands.Author.UpdateAuthorCommand;

public sealed record UpdateAuthorCommand(UpdateAuthor UpdateAuthorDto) : IRequest<AuthorDTO>;
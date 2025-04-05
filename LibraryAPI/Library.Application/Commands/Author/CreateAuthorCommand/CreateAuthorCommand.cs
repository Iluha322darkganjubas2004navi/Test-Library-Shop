using Library.Domain.DTOs;
using MediatR;

namespace Library.Application.Commands.Author.CreateAuthorCommand;

public sealed record CreateAuthorCommand(CreateAuthor CreateAuthorDto) : IRequest<AuthorDTO>;
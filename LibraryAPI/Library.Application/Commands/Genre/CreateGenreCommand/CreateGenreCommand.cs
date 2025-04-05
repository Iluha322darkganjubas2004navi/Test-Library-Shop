using Library.Domain.DTOs;
using MediatR;

namespace Library.Application.Commands.Genre.CreateGenreCommand;

public sealed record CreateGenreCommand(CreateGenre CreateGenreDto) : IRequest<GenreDTO>;
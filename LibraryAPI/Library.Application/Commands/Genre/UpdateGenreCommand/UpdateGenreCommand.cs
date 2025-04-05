using Library.Domain.DTOs;
using MediatR;

namespace Library.Application.Commands.Genre.UpdateGenreCommand;

public sealed record UpdateGenreCommand(UpdateGenre UpdateGenreDto) : IRequest<GenreDTO>;
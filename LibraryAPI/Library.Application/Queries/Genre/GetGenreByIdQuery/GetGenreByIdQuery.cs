using Library.Application.DTOs;
using Library.Domain.DTOs;
using MediatR;

namespace Library.Application.Queries.Genre.GetGenreByIdQuery;

public record GetGenreByIdQuery(Guid GenreId) : IRequest<GenreDTO>;
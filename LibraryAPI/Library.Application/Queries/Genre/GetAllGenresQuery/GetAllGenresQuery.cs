using Library.Domain.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Library.Application.Queries.Genre.GetAllGenresQuery;

public sealed record GetAllGenresQuery() : IRequest<IEnumerable<GenreDTO>>;
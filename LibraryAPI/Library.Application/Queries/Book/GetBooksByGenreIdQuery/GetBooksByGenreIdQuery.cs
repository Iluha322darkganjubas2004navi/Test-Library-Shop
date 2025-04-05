using Library.Domain.DTOs;
using MediatR;
using System;
using System.Collections.Generic;

namespace Library.Application.Queries.Book.GetBooksByGenreIdQuery;

public sealed record GetBooksByGenreIdQuery(Guid GenreId) : IRequest<IEnumerable<BookDTO>>;
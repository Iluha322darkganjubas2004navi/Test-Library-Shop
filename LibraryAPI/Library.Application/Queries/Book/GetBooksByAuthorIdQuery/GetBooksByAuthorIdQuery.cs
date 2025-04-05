using Library.Domain.DTOs;
using MediatR;
using System;
using System.Collections.Generic;

namespace Library.Application.Queries.Book.GetBooksByAuthorIdQuery;

public sealed record GetBooksByAuthorIdQuery(Guid AuthorId) : IRequest<IEnumerable<BookDTO>>;
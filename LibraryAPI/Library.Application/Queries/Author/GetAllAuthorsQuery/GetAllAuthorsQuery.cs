using Library.Domain.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Library.Application.Queries.Author.GetAllAuthorsQuery;

public sealed record GetAllAuthorsQuery() : IRequest<IEnumerable<AuthorDTO>>;
using Library.Domain.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Library.Application.Queries.Book.GetAllBooksQuery;

public sealed record GetAllBooksQuery() : IRequest<List<BookDTO>>;
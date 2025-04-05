using Library.Domain.DTOs;
using MediatR;

namespace Library.Application.Queries.Book.GetBookByIsbnQuery;

public sealed record GetBookByIsbnQuery(string Isbn) : IRequest<BookDTO>;

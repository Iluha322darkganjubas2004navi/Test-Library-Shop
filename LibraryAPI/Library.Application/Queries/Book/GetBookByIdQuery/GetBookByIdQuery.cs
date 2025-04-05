using Library.Domain.DTOs;
using MediatR;
using System;

namespace Library.Application.Queries.Book.GetBookByIdQuery;

public sealed record GetBookByIdQuery(Guid BookId) : IRequest<BookDTO>;
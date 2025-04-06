using Library.Domain.DTOs;
using MediatR;
using System;
using System.Collections.Generic;

namespace Library.Application.Queries.BookBorrowing.GetAllBookBorrowingsByUserIdQuery;

public sealed record GetAllBookBorrowingsByUserIdQuery(Guid UserId) : IRequest<IEnumerable<BookBorrowingDTO>>;
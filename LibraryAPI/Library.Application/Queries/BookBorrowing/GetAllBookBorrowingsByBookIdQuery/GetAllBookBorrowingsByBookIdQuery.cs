using Library.Domain.DTOs;
using MediatR;
using System;
using System.Collections.Generic;

namespace Library.Application.Queries.BookBorrowing.GetAllBookBorrowingsByBookIdQuery;

public sealed record GetAllBookBorrowingsByBookIdQuery(Guid BookId) : IRequest<IEnumerable<BookBorrowingDTO>>;
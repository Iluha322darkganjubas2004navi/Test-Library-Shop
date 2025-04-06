using Library.Application.DTOs;
using Library.Domain.DTOs;
using MediatR;
using System;

namespace Library.Application.Queries.BookBorrowing.GetBookBorrowingByIdQuery;

public record GetBookBorrowingByIdQuery(Guid BorrowingId) : IRequest<BookBorrowingDTO>;
using Library.Domain.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Library.Application.Queries.Book.GetAllBooksQuery;

public class GetAllBooksQuery : IRequest<PaginatedResult<BookDTO>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
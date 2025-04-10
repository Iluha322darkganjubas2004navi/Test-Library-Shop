using AutoMapper;
using Library.Domain.DTOs;
using Library.Infrastructure.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Queries.Book.GetAllBooksQuery;

public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, PaginatedResult<BookDTO>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public GetAllBooksQueryHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<BookDTO>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        var allBooks = await _bookRepository.GetAllBooksWithDetailsAsync(cancellationToken);

        var totalCount = allBooks.Count;
        var paginatedBooks = allBooks
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();

        return new PaginatedResult<BookDTO>
        {
            Items = _mapper.Map<IEnumerable<BookDTO>>(paginatedBooks),
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }
}

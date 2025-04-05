using AutoMapper;
using Library.Application.Queries.Book.GetAllBooksQuery;
using Library.Domain.DTOs;
using Library.Infrastructure.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Queries.Book.GetAllBooksQuery;

public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, IEnumerable<BookDTO>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public GetAllBooksQueryHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BookDTO>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<BookDTO>>(books);
    }
}
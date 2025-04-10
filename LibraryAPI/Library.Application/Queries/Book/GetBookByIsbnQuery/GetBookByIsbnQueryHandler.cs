using AutoMapper;
using FluentValidation;
using Library.Application.Exceptions;
using Library.Application.Queries.Book.GetBookByIsbnQuery;
using Library.Domain.DTOs;
using Library.Infrastructure.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Queries.Book.GetBookByIsbnQuery;

public class GetBookByIsbnQueryHandler : IRequestHandler<GetBookByIsbnQuery, BookDTO>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<GetBookByIsbnQuery> _validator;

    public GetBookByIsbnQueryHandler(IBookRepository bookRepository, IMapper mapper, IValidator<GetBookByIsbnQuery> validator)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<BookDTO> Handle(GetBookByIsbnQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var existingBook = await _bookRepository.GetBookByIsbnAsync(request.Isbn, cancellationToken);

        if (existingBook == null)
        {
            throw new NotFoundException($"Book with ISBN '{request.Isbn}' not found.");
        }

        return _mapper.Map<BookDTO>(existingBook);
    }
}
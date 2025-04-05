using AutoMapper;
using FluentValidation;
using Library.Application.Exceptions;
using Library.Application.Queries.Book.GetBooksByAuthorIdQuery;
using Library.Domain.DTOs;
using Library.Infrastructure.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Queries.Book.GetBooksByAuthorIdQuery;

public class GetBooksByAuthorIdQueryHandler : IRequestHandler<GetBooksByAuthorIdQuery, IEnumerable<BookDTO>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<GetBooksByAuthorIdQuery> _validator;

    public GetBooksByAuthorIdQueryHandler(IBookRepository bookRepository, IAuthorRepository authorRepository, IMapper mapper, IValidator<GetBooksByAuthorIdQuery> validator)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<IEnumerable<BookDTO>> Handle(GetBooksByAuthorIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var existingAuthor = await _authorRepository.GetByIdAsync(request.AuthorId);
        if (existingAuthor == null)
        {
            throw new NotFoundException($"Author with id '{request.AuthorId}' not found.");
        }

        var books = await _bookRepository.GetBooksByAuthorIdAsync(request.AuthorId);
        return _mapper.Map<IEnumerable<BookDTO>>(books);
    }
}
using AutoMapper;
using FluentValidation;
using Library.Application.Exceptions;
using Library.Application.Queries.Book.GetBooksByGenreIdQuery;
using Library.Domain.DTOs;
using Library.Infrastructure.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Queries.Book.GetBooksByGenreIdQuery;

public class GetBooksByGenreIdQueryHandler : IRequestHandler<GetBooksByGenreIdQuery, IEnumerable<BookDTO>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IGenreRepository _genreRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<GetBooksByGenreIdQuery> _validator;

    public GetBooksByGenreIdQueryHandler(IBookRepository bookRepository, IGenreRepository genreRepository, IMapper mapper, IValidator<GetBooksByGenreIdQuery> validator)
    {
        _bookRepository = bookRepository;
        _genreRepository = genreRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<IEnumerable<BookDTO>> Handle(GetBooksByGenreIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var existingGenre = await _genreRepository.GetByIdAsync(request.GenreId);
        if (existingGenre == null)
        {
            throw new NotFoundException($"Genre with id '{request.GenreId}' not found.");
        }

        var books = await _bookRepository.GetBooksByGenreIdAsync(request.GenreId);

        return _mapper.Map<IEnumerable<BookDTO>>(books);
    }
}
using AutoMapper;
using FluentValidation;
using Library.Application.Commands.Book.CreateBookCommand;
using Library.Application.Exceptions;
using Library.Domain.DTOs;
using Library.Domain.Entities;
using Library.Infrastructure.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Commands.Book.CreateBookCommand;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, BookDTO>
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IGenreRepository _genreRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateBookCommand> _validator;

    public CreateBookCommandHandler(IBookRepository bookRepository,
                                    IAuthorRepository authorRepository,
                                    IGenreRepository genreRepository,
                                    IMapper mapper,
                                    IValidator<CreateBookCommand> validator)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
        _genreRepository = genreRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<BookDTO> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var existingAuthor = await _authorRepository.GetByIdAsync(request.CreateBookDto.AuthorId, cancellationToken);
        if (existingAuthor == null)
        {
            throw new NotFoundException($"Author with id '{request.CreateBookDto.AuthorId}' not found.");
        }

        var allGenres = await _genreRepository.GetAllAsync(cancellationToken);

        var existingGenres = allGenres.Where(g => request.CreateBookDto.GenreIds.Contains(g.Id)).ToList();

        if (existingGenres.Count != request.CreateBookDto.GenreIds.Count)
        {
            var foundGenreIds = existingGenres.Select(g => g.Id);
            var notFoundGenreIds = request.CreateBookDto.GenreIds.Except(foundGenreIds);
            throw new NotFoundException($"Genres with ids '{string.Join(", ", notFoundGenreIds)}' not found.");
        }

        var book = _mapper.Map<Domain.Entities.Book>(request.CreateBookDto);
        book.Author = existingAuthor;

        await _bookRepository.AddAsync(book, cancellationToken);

        foreach (var genre in existingGenres)
        {
            book.Genres.Add(genre);
        }
        await _bookRepository.UpdateAsync(book, cancellationToken);

        return _mapper.Map<BookDTO>(book);
    }
}
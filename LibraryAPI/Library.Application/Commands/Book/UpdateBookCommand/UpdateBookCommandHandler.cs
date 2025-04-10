using AutoMapper;
using FluentValidation;
using Library.Application.Commands.Book.UpdateBookCommand;
using Library.Application.Exceptions;
using Library.Domain.DTOs;
using Library.Domain.Entities;
using Library.Infrastructure.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Commands.Book.UpdateBookCommand;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, BookDTO>
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IGenreRepository _genreRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateBookCommand> _validator;

    public UpdateBookCommandHandler(IBookRepository bookRepository,
                                    IAuthorRepository authorRepository,
                                    IGenreRepository genreRepository,
                                    IMapper mapper,
                                    IValidator<UpdateBookCommand> validator)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
        _genreRepository = genreRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<BookDTO> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var existingBook = await _bookRepository.GetByIdAsync(request.UpdateBookDto.Id, cancellationToken);
        if (existingBook == null)
        {
            throw new NotFoundException($"Book with id '{request.UpdateBookDto.Id}' not found.");
        }

        var existingAuthor = await _authorRepository.GetByIdAsync(request.UpdateBookDto.AuthorId, cancellationToken);
        if (existingAuthor == null)
        {
            throw new NotFoundException($"Author with id '{request.UpdateBookDto.AuthorId}' not found.");
        }

        var allGenres = await _genreRepository.GetAllAsync(cancellationToken);
        var existingGenres = allGenres.Where(g => request.UpdateBookDto.GenreIds.Contains(g.Id)).ToList();

        if (existingGenres.Count != request.UpdateBookDto.GenreIds.Count)
        {
            var foundGenreIds = existingGenres.Select(g => g.Id);
            var notFoundGenreIds = request.UpdateBookDto.GenreIds.Except(foundGenreIds);
            throw new NotFoundException($"Genres with ids '{string.Join(", ", notFoundGenreIds)}' not found.");
        }

        _mapper.Map(request.UpdateBookDto, existingBook);
        existingBook.Author = existingAuthor;

        existingBook.Genres.Clear();
        foreach (var genre in existingGenres)
        {
            existingBook.Genres.Add(genre);
        }

        await _bookRepository.UpdateAsync(existingBook, cancellationToken);

        return _mapper.Map<BookDTO>(existingBook);
    }
}
using AutoMapper;
using FluentValidation;
using Library.Application.DTOs;
using Library.Application.Exceptions;
using Library.Domain.DTOs;
using Library.Domain.Entities;
using Library.Infrastructure.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Queries.Genre.GetGenreByIdQuery;

public class GetGenreByIdQueryHandler : IRequestHandler<GetGenreByIdQuery, GenreDTO>
{
    private readonly IGenreRepository _genreRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<GetGenreByIdQuery> _validator;

    public GetGenreByIdQueryHandler(IGenreRepository genreRepository, IMapper mapper, IValidator<GetGenreByIdQuery> validator)
    {
        _genreRepository = genreRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<GenreDTO> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        var genre = await _genreRepository.GetByIdAsync(request.GenreId, cancellationToken);

        if (genre == null)
        {
            throw new NotFoundException($"Genre with id '{request.GenreId}' not found.");
        }

        return _mapper.Map<GenreDTO>(genre);
    }
}
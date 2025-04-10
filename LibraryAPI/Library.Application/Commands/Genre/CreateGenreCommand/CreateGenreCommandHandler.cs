using AutoMapper;
using FluentValidation;
using Library.Application.Commands.Genre.CreateGenreCommand;
using Library.Application.Exceptions;
using Library.Domain.DTOs;
using Library.Domain.Entities;
using Library.Infrastructure.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Commands.Genre.CreateGenreCommand;

public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, GenreDTO>
{
    private readonly IGenreRepository _genreRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateGenreCommand> _validator;

    public CreateGenreCommandHandler(IGenreRepository genreRepository, IMapper mapper, IValidator<CreateGenreCommand> validator)
    {
        _genreRepository = genreRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<GenreDTO> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var genre = _mapper.Map<Domain.Entities.Genre>(request.CreateGenreDto);
        await _genreRepository.AddAsync(genre, cancellationToken);
        return _mapper.Map<GenreDTO>(genre);
    }
}
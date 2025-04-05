using AutoMapper;
using FluentValidation;
using Library.Application.Commands.Genre.UpdateGenreCommand;
using Library.Application.Exceptions;
using Library.Domain.DTOs;
using Library.Domain.Entities;
using Library.Infrastructure.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Commands.Genre.UpdateGenreCommand;

public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand, GenreDTO>
{
    private readonly IGenreRepository _genreRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateGenreCommand> _validator;

    public UpdateGenreCommandHandler(IGenreRepository genreRepository, IMapper mapper, IValidator<UpdateGenreCommand> validator)
    {
        _genreRepository = genreRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<GenreDTO> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var existingGenre = await _genreRepository.GetByIdAsync(request.UpdateGenreDto.Id);
        if (existingGenre == null)
        {
            throw new NotFoundException($"Genre with id '{request.UpdateGenreDto.Id}' not found.");
        }

        _mapper.Map(request.UpdateGenreDto, existingGenre);
        await _genreRepository.UpdateAsync(existingGenre);
        return _mapper.Map<GenreDTO>(existingGenre);
    }
}
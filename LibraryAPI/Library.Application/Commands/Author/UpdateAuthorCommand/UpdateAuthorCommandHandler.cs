using AutoMapper;
using FluentValidation;
using Library.Application.Commands.Author.UpdateAuthorCommand;
using Library.Application.Exceptions;
using Library.Domain.DTOs;
using Library.Domain.Entities;
using Library.Infrastructure.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Commands.Author.UpdateAuthorCommand;

public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, AuthorDTO>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateAuthorCommand> _validator;

    public UpdateAuthorCommandHandler(IAuthorRepository authorRepository, IMapper mapper, IValidator<UpdateAuthorCommand> validator)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<AuthorDTO> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var existingAuthor = await _authorRepository.GetByIdAsync(request.UpdateAuthorDto.Id);
        if (existingAuthor == null)
        {
            throw new NotFoundException($"Author with id '{request.UpdateAuthorDto.Id}' not found.");
        }

        _mapper.Map(request.UpdateAuthorDto, existingAuthor);
        await _authorRepository.UpdateAsync(existingAuthor);
        return _mapper.Map<AuthorDTO>(existingAuthor);
    }
}
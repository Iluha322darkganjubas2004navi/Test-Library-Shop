using AutoMapper;
using FluentValidation;
using Library.Application.Commands.Author.CreateAuthorCommand;
using Library.Application.Exceptions;
using Library.Domain.DTOs;
using Library.Domain.Entities;
using Library.Infrastructure.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Commands.Author.CreateAuthorCommand;

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, AuthorDTO>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateAuthorCommand> _validator;

    public CreateAuthorCommandHandler(IAuthorRepository authorRepository, IMapper mapper, IValidator<CreateAuthorCommand> validator)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<AuthorDTO> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var author = _mapper.Map<Domain.Entities.Author>(request.CreateAuthorDto);
        var createdAuthor = await _authorRepository.AddAsync(author, cancellationToken);
        return _mapper.Map<AuthorDTO>(createdAuthor);
    }
}
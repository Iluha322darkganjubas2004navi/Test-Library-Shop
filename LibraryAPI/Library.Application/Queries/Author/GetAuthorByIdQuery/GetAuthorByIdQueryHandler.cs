using AutoMapper;
using FluentValidation;
using Library.Application.Exceptions;
using Library.Application.Queries.Author.GetAuthorByIdQuery;
using Library.Domain.DTOs;
using Library.Infrastructure.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Queries.Author.GetAuthorByIdQuery;

public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, AuthorDTO>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<GetAuthorByIdQuery> _validator;

    public GetAuthorByIdQueryHandler(IAuthorRepository authorRepository, IMapper mapper, IValidator<GetAuthorByIdQuery> validator)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<AuthorDTO> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
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

        return _mapper.Map<AuthorDTO>(existingAuthor);
    }
}
﻿using AutoMapper;
using Library.Application.Queries.Author.GetAllAuthorsQuery;
using Library.Domain.DTOs;
using Library.Infrastructure.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Queries.Author.GetAllAuthorsQuery;

public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, IEnumerable<AuthorDTO>>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;

    public GetAllAuthorsQueryHandler(IAuthorRepository authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AuthorDTO>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
    {
        var authors = await _authorRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<AuthorDTO>>(authors);
    }
}
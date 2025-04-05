using AutoMapper;
using Library.Application.Queries.Genre.GetAllGenresQuery;
using Library.Domain.DTOs;
using Library.Infrastructure.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Queries.Genre.GetAllGenresQuery;

public class GetAllGenresQueryHandler : IRequestHandler<GetAllGenresQuery, IEnumerable<GenreDTO>>
{
    private readonly IGenreRepository _genreRepository;
    private readonly IMapper _mapper;

    public GetAllGenresQueryHandler(IGenreRepository genreRepository, IMapper mapper)
    {
        _genreRepository = genreRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GenreDTO>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
    {
        var genres = await _genreRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<GenreDTO>>(genres);
    }
}
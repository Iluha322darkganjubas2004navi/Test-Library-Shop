using Library.Application.Commands.Genre.DeleteGenreCommand;
using Library.Application.Exceptions;
using Library.Infrastructure.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Application.Commands.Genre.DeleteGenreCommand;

public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand, bool>
{
    private readonly IGenreRepository _genreRepository;

    public DeleteGenreCommandHandler(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<bool> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
    {
        var existingGenre = await _genreRepository.GetByIdAsync(request.Id, cancellationToken);
        if (existingGenre == null)
        {
            throw new NotFoundException($"Genre with id '{request.Id}' not found.");
        }

        await _genreRepository.DeleteAsync(existingGenre, cancellationToken);
        return true;
    }
}

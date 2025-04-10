using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories;

[AutoInterface(Inheritance = [typeof(IBaseRepository<Book>)])]
public class BookRepository(AppDbContext dbContext) : BaseRepository<Book>(dbContext), IBookRepository
{
    public async Task<List<Book>> GetBooksByGenreIdAsync(Guid genreId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(book => book.Genres.Any(genre => genre.Id == genreId))
            .ToListAsync(cancellationToken);
    }
    public async Task<List<Book>> GetBooksByAuthorIdAsync(Guid authorId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(book => book.AuthorId == authorId)
            .ToListAsync(cancellationToken);
    }
    public async Task<Book?> GetBookByIsbnAsync(string isbn, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(book => book.ISBN == isbn, cancellationToken);
    }
    public async Task<Book?> GetBookByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(b => b.Genres)
            .Include(b => b.Author)
            .FirstOrDefaultAsync(book => book.Id == id, cancellationToken);
    }
    public async Task<List<Book>> GetAllBooksWithDetailsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(b => b.Genres)
            .Include(b => b.Author)
            .ToListAsync(cancellationToken);
    }
}
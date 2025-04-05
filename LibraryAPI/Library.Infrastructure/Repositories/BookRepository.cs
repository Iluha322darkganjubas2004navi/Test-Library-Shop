namespace Library.Infrastructure.Repositories;

[AutoInterface(Inheritance = [typeof(IBaseRepository<Book>)])]
public class BookRepository(AppDbContext dbContext) : BaseRepository<Book>(dbContext), IBookRepository
{
    public async Task<List<Book>> GetBooksByGenreIdAsync(Guid genreId)
    {
        return await _dbSet
            .Where(book => book.Genres.Any(genre => genre.Id == genreId))
            .ToListAsync();
    }
    public async Task<List<Book>> GetBooksByAuthorIdAsync(Guid authorId)
    {
        return await _dbSet
            .Where(book => book.AuthorId == authorId)
            .ToListAsync();
    }
    public async Task<Book?> GetBookByIsbnAsync(string isbn)
    {
        return await _dbSet.FirstOrDefaultAsync(book => book.ISBN == isbn);
    }
}
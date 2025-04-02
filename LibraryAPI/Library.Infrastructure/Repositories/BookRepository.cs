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
}
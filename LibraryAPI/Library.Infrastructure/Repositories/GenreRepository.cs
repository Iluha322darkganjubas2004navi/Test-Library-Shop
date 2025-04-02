namespace Library.Infrastructure.Repositories;

[AutoInterface(Inheritance = [typeof(IBaseRepository<Genre>)])]
public class GenreRepository(AppDbContext dbContext) : BaseRepository<Genre>(dbContext), IGenreRepository
{
    public async Task<List<Genre>> GetGenresByBookIdAsync(Guid bookId)
    {
        return await _dbSet
            .Where(genre => genre.Books.Any(book => book.Id == bookId))
            .ToListAsync();
    }
}
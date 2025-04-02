namespace Library.Infrastructure.Repositories;

[AutoInterface(Inheritance = [typeof(IBaseRepository<Author>)])]
public class AuthorRepository(AppDbContext dbContext) : BaseRepository<Author>(dbContext), IAuthorRepository
{
    public async Task<Author> GetAuthorByNameAsync(string firstName, string lastName)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.FirstName.Equals(firstName) && c.LastName.Equals(lastName));
    }
}

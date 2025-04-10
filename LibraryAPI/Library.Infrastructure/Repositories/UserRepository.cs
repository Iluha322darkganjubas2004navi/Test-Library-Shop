namespace Library.Infrastructure.Repositories;

[AutoInterface(Inheritance = [typeof(IBaseRepository<User>)])]
public class UserRepository(AppDbContext dbContext) : BaseRepository<User>(dbContext), IUserRepository
{
    public async Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email.Equals(email), cancellationToken);
    }

    public async Task<User> GetUserByLoginAsync(string Login, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Name.Equals(Login), cancellationToken);
    }

    public async Task<User> GetUserAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(predicate).FirstOrDefaultAsync(cancellationToken);
    }
}
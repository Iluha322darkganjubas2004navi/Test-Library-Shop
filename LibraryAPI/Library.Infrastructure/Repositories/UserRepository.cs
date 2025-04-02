namespace Library.Infrastructure.Repositories;

[AutoInterface(Inheritance = [typeof(IBaseRepository<User>)])]
public class UserRepository(AppDbContext dbContext) : BaseRepository<User>(dbContext), IUserRepository
{
    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email.Equals(email));
    }

    public async Task<User> GetUserByLoginAsync(string Login)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Name.Equals(Login));
    }

    public async Task<User> GetUserAsync(Expression<Func<User, bool>> predicate)
    {
        return await _dbSet.Where(predicate).FirstOrDefaultAsync();
    }
}
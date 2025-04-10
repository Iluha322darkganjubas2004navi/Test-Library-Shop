namespace Library.Infrastructure.Repositories;

[AutoInterface(Inheritance = [typeof(IBaseRepository<RefreshToken>)])]
public class RefreshTokenRepository(AppDbContext dbContext) : BaseRepository<RefreshToken>(dbContext), IRefreshTokenRepository
{
    public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(rt => rt.Token == token, cancellationToken);
    }

    public async Task MarkAsUsedAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
    {
        if (refreshToken != null)
        {
            refreshToken.IsUsed = true;
            _context.Update(refreshToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task MarkAsRevokedAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
    {
        if (refreshToken != null)
        {
            refreshToken.IsRevoked = true;
            _context.Update(refreshToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task RevokeAllUserTokensAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var tokensToRevoke = await _dbSet
            .Where(rt => rt.UserId == userId && !rt.IsRevoked)
            .ToListAsync(cancellationToken);

        if (tokensToRevoke.Any())
        {
            foreach (var token in tokensToRevoke)
            {
                token.IsRevoked = true;
                _context.Update(token);
            }
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
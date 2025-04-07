using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories;

[AutoInterface(Inheritance = [typeof(IBaseRepository<RefreshToken>)])]
public class RefreshTokenRepository(AppDbContext dbContext) : BaseRepository<RefreshToken>(dbContext), IRefreshTokenRepository
{
    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        return await _dbSet.FirstOrDefaultAsync(rt => rt.Token == token);
    }

    public async Task MarkAsUsedAsync(RefreshToken refreshToken)
    {
        if (refreshToken != null)
        {
            refreshToken.IsUsed = true;
            _context.Update(refreshToken);
            await _context.SaveChangesAsync();
        }
    }

    public async Task MarkAsRevokedAsync(RefreshToken refreshToken)
    {
        if (refreshToken != null)
        {
            refreshToken.IsRevoked = true;
            _context.Update(refreshToken);
            await _context.SaveChangesAsync();
        }
    }

    public async Task RevokeAllUserTokensAsync(Guid userId)
    {
        var tokensToRevoke = await _dbSet
            .Where(rt => rt.UserId == userId && !rt.IsRevoked)
            .ToListAsync();

        if (tokensToRevoke.Any())
        {
            foreach (var token in tokensToRevoke)
            {
                token.IsRevoked = true;
                _context.Update(token);
            }
            await _context.SaveChangesAsync();
        }
    }
}
namespace Library.Infrastructure.Repositories;

[AutoInterface(Inheritance = [typeof(IBaseRepository<BookBorrowing>)])]
public class BookBorrowingRepository(AppDbContext dbContext) : BaseRepository<BookBorrowing>(dbContext), IBookBorrowingRepository
{
    public async Task<List<BookBorrowing>> GetBookBorrowingsByBookIdAsync(Guid bookId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(borrowing => borrowing.BookId == bookId)
            .Include(b => b.Book)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<BookBorrowing>> GetBookBorrowingsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(borrowing => borrowing.UserId == userId)
            .Include(b => b.Book)
            .ToListAsync(cancellationToken);
    }
}
namespace Library.Infrastructure.Repositories;

[AutoInterface(Inheritance = [typeof(IBaseRepository<BookBorrowing>)])]
public class BookBorrowingRepository(AppDbContext dbContext) : BaseRepository<BookBorrowing>(dbContext), IBookBorrowingRepository;
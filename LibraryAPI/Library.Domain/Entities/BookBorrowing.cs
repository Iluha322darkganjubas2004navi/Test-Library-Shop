using Library.Domain.Entities.Base;

namespace Library.Domain.Entities;
public class BookBorrowing : BaseEntity
{
    public Guid BookId { get; set; }
    public Book Book { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public DateTime BorrowedDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public DateTime? ReturnedAt { get; set; }
}
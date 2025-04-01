using Library.Domain.Entities.Base;

namespace Library.Domain.Entities;
public class BookBorrowing : BaseEntity
{
    public Book Book { get; set; }
    public User User { get; set; }
    public DateTime BorrowedDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public DateTime? ReturnedAt { get; set; }
}
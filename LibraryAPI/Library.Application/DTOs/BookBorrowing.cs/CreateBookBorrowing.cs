namespace Library.Domain.DTOs;

public class CreateBookBorrowing
{
    public Guid BookId { get; set; }
    public Guid UserId { get; set; }
    public DateTime BorrowedDate { get; set; }
    public DateTime ReturnDate { get; set; }
}
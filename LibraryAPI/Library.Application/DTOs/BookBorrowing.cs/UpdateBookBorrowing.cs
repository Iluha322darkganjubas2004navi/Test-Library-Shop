namespace Library.Domain.DTOs;

public class UpdateBookBorrowing
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid UserId { get; set; }
    public DateTime BorrowedDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public DateTime? ReturnedAt { get; set; }
}
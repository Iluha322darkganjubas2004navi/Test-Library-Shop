namespace Library.Domain.DTOs;

public class CreateBook
{
    public string ISBN { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid AuthorId { get; set; }
    public DateTime? BorrowedDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public List<Guid> GenreIds { get; set; } = new List<Guid>();
    public bool IsBorrowed { get; set; }
}
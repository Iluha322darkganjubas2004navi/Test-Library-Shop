namespace Library.Domain.DTOs;

public class BookDTO
{
    public Guid Id { get; set; }
    public string ISBN { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid AuthorId { get; set; }
    public DateTime? BorrowedDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public List<GenreDTO> Genres { get; set; } = new List<GenreDTO>();
    public bool IsBorrowed { get; set; }
}
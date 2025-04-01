using Library.Domain.Entities.Base;
using Library.Domain.Enums;

namespace Library.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
    public List<BookBorrowing> BorrowedBooks { get; set; } = new List<BookBorrowing>();
}
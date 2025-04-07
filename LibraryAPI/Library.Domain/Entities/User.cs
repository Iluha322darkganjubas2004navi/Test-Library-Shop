using Library.Domain.Entities.Base;
using Library.Domain.Enums;

namespace Library.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
    public ICollection<BookBorrowing> BorrowedBooks { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}
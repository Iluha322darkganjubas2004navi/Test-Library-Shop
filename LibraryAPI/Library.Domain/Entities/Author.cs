using Library.Domain.Entities.Base;

namespace Library.Domain.Entities;
public class Author : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Country { get; set; }
    public ICollection<Book> Books { get; set; }
}

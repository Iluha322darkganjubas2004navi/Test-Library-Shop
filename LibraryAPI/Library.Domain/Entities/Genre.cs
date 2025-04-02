using Library.Domain.Entities.Base;

namespace Library.Domain.Entities;

public class Genre : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Book> Books { get; set; }
}


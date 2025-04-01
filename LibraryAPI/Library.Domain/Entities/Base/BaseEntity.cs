using Library.Domain.Entities.Interfaces;

namespace Library.Domain.Entities.Base;
public class BaseEntity : IHasId
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public DateTime? DateUpdated { get; set; }
}
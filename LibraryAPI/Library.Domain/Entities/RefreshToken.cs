using Library.Domain.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Domain.Entities;

public class RefreshToken : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public string Token { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsUsed { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime AddedDate { get; set; } = DateTime.UtcNow;
    public string? ReplacedByToken { get; set; }
}
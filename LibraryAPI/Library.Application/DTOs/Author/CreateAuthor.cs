namespace Library.Domain.DTOs;

public class CreateAuthor
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Country { get; set; }
}
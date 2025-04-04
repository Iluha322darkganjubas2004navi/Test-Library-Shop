using Library.Domain.Enums;

namespace Library.Application.DTOs.User
{
    public class UpdateUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
    }

}

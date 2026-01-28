using System.ComponentModel.DataAnnotations;

namespace agent.DTOs
{
    public class UserOutDTO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
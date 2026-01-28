using System.ComponentModel.DataAnnotations;

namespace agent.DTOs
{
    public class LogInDTO
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace agent.DTOs
{
    public class CategoryOutDTO
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }
    }
}
using agent.entityClasses;
using System.ComponentModel.DataAnnotations;

namespace agent.DTOs
{
    public class CategoryInDTO
    {
        [Required]
        public string CategoryName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace agent.DTOs
{
    public class RegionDTO
    {
        [Required]
        [StringLength(100)]
        public string RegionName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }
}
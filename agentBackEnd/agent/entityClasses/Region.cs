using System.ComponentModel.DataAnnotations;

namespace agent.entityClasses
{
    public class Region
    {
        [Key]
        public int RegionId { get; set; }

        [Required]
        [StringLength(100)]
        public string RegionName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public virtual ICollection<Country> Countries { get; set; } = new List<Country>();
    }
}
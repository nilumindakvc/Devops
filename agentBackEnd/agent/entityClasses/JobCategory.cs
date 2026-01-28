using System.ComponentModel.DataAnnotations;

namespace agent.entityClasses
{
    public class JobCategory
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation Properties
        public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}
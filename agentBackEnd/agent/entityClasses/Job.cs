using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace agent.entityClasses
{
    public class Job
    {
        [Key]
        public int JobId { get; set; }

        [Required]
        [StringLength(200)]
        public string JobTitle { get; set; }

        [Column(TypeName = "text")]
        public string JobDescription { get; set; }

        [ForeignKey("JobCategory")]
        public int CategoryId { get; set; }

        [ForeignKey("Agency")]
        public int AgencyId { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }

        [StringLength(100)]
        public string SalaryRange { get; set; }

        public int RegionId { get; set; }

        [Column(TypeName = "text")]
        public string Requirements { get; set; }

        public bool IsUrgent { get; set; } = false;

        public bool OpenedUrgently { get; set; } = false;
        public bool IsActive { get; set; } = true;

        public DateTime? Deadline { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public virtual JobCategory JobCategory { get; set; }

        public virtual Agency Agency { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
    }
}
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace agent.entityClasses
{
    public class JobApplication
    {
        [Key]
        public int ApplicationId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Job")]
        public int JobId { get; set; }

        public ApplicationStatus ApplicationStatus { get; set; } = ApplicationStatus.Applied;

        [Column(TypeName = "text")]
        public string CoverLetter { get; set; }

        public DateTime AppliedDate { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public virtual User User { get; set; }

        public virtual Job Job { get; set; }
    }
}
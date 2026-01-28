using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace agent.entityClasses
{
    public class AgencyReview
    {
        [Key]
        public int ReviewId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Agency")]
        public int AgencyId { get; set; }

        [Column(TypeName = "text")]
        public string ReviewText { get; set; }

        public string ServiceNumber { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public virtual User User { get; set; }

        public virtual Agency Agency { get; set; }
    }
}
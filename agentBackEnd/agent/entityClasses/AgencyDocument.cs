using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace agent.entityClasses
{
    public class AgencyDocument
    {
        [Key]
        public int DocumentId { get; set; }

        [ForeignKey("Agency")]
        public int AgencyId { get; set; }

        [Required]
        [StringLength(100)]
        public string DocumentType { get; set; }

        [Required]
        [StringLength(200)]
        public string DocumentName { get; set; }

        [Required]
        [StringLength(500)]
        public string FilePath { get; set; }

        public DateTime UploadedDate { get; set; } = DateTime.UtcNow;

        public bool IsVerified { get; set; } = false;

        // Navigation Properties
        public virtual Agency Agency { get; set; }
    }
}
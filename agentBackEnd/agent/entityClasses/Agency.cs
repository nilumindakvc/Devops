using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace agent.entityClasses
{
    public class Agency
    {
        [Key]
        public int AgencyId { get; set; }

        [Required]
        [StringLength(100)]
        public string AgencyName { get; set; }

        [StringLength(50)]
        public string LicenseNumber { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(100)]
        public string Website { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        public RegistrationStatus RegistrationStatus { get; set; } = RegistrationStatus.Pending;

        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        public DateTime? ApprovedDate { get; set; }

        public bool IsActive { get; set; } = false;

        [Column(TypeName = "decimal(3,2)")]
        public decimal AverageRating { get; set; } = 0;

        public int TotalReviews { get; set; } = 0;

        public string Password { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public String LogoPath { get; set; } = String.Empty;
        public string Doc1Path { get; set; } = String.Empty;
        public String Doc2Path { get; set; } = string.Empty;

        // Navigation Properties
        public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();

        public virtual ICollection<AgencyReview> AgencyReviews { get; set; } = new List<AgencyReview>();
        public virtual ICollection<AgencyCountry> AgencyCountries { get; set; } = new List<AgencyCountry>();
        public virtual ICollection<AgencyDocument> AgencyDocuments { get; set; } = new List<AgencyDocument>();
    }
}
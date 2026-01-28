using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace agent.entityClasses
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }

        [Required]
        [StringLength(100)]
        public string CountryName { get; set; }

        [StringLength(10)]
        public string CountryCode { get; set; }

        [ForeignKey("Region")]
        public int RegionId { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public virtual Region Region { get; set; }

        public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
        public virtual ICollection<AgencyCountry> AgencyCountries { get; set; } = new List<AgencyCountry>();
    }
}
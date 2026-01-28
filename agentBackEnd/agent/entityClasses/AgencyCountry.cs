using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace agent.entityClasses
{
    public class AgencyCountry
    {
        [Key]
        public int AgencyCountryId { get; set; }

        [ForeignKey("Agency")]
        public int AgencyId { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public virtual Agency Agency { get; set; }

        public virtual Country Country { get; set; }
    }
}
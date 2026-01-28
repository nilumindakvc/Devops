using agent.entityClasses;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace agent.DTOs
{
    public class CountryDTO
    {
        [Required]
        [StringLength(100)]
        public string CountryName { get; set; }

        [StringLength(10)]
        public string CountryCode { get; set; }

        [ForeignKey("Region")]
        [Required]
        public int RegionId { get; set; }

        public int CountryId { get; set; }
    }
}
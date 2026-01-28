using agent.entityClasses;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace agent.DTOs
{
    public class AgencyCountryDTO
    {
        [Required]
        public int AgencyId { get; set; }

        [Required]
        public int CountryId { get; set; }
    }
}
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace agent.DTOs
{
    public class CountryComDTO
    {
        public string CountryName { get; set; }
        public int RegionId { get; set; }
        public List<AgencyOutDTO> AgencyList { get; set; }
    }
}
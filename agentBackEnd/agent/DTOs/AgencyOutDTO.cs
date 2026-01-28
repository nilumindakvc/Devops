using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace agent.DTOs
{
    public class AgencyOutDTO
    {
        public int AgencyId { get; set; }
        public string AgencyName { get; set; }

        public string LicenseNumber { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Website { get; set; }

        public string Description { get; set; }

        public decimal AverageRating { get; set; } = 0;
    }
}
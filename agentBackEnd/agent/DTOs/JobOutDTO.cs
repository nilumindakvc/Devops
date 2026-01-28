using agent.entityClasses;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace agent.DTOs
{
    public class JobOutDTO
    {
        public int JobId { get; set; }
        public string JobTitle { get; set; }

        public string JobDescription { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int AgencyId { get; set; }
        public string AgencyName { get; set; }

        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public int RegionId { get; set; }

        public string RegionName { get; set; }

        public string SalaryRange { get; set; }

        public string Requirements { get; set; }

        public bool IsUrgent { get; set; } = false;
        public bool OpenedUrgently { get; set; } = false;

        public DateTime? Deadline { get; set; }
    }
}
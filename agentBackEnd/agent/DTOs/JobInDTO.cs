namespace agent.DTOs
{
    public class JobInDTO
    {
        public string JobTitle { get; set; }

        public string JobDescription { get; set; }

        public int CategoryId { get; set; }

        public int AgencyId { get; set; }

        public int CountryId { get; set; }

        public int RegionId { get; set; }

        public string SalaryRange { get; set; }

        public string Requirements { get; set; }

        public bool IsUrgent { get; set; } = false;
        public bool OpenedUrgently { get; set; } = false;

        public DateTime? Deadline { get; set; }
    }
}
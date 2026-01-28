namespace agent.DTOs
{
    public class ReviewDTO
    {
        public int AgencyId { get; set; }
        public int UserId { get; set; }
        public string ReviewText { get; set; }
        public string ServiceNumber { get; set; }
    }
}
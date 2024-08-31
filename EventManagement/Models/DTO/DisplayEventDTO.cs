namespace EventManagement.Models.DTO
{
    public class DisplayEventDTO
    {
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime MaxDate { get; set; }
        public int MaxParticipants { get; set; }
        public Guid CreatedBy { get; set; }
    }
}

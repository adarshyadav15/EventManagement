namespace EventManagement.Models.DTO
{
    public class CreateEventDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime MaxDate { get; set; }
        public int MaxParticipants { get; set; }
        public Guid CreatedBy { get; set; }
    }
}

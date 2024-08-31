namespace EventManagement.Models.Domain
{
    public class Feedback
    {
        public Guid FeedbackId { get; set; }
        public Guid EventId { get; set; }
        public Guid UserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }

        // Navigation properties
        public Event Event { get; set; }
        public User User { get; set; }
    }
}

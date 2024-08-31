using EventManagement.Models.Domain;

namespace EventManagement.Models.DTO
{
    public class FeedbackDTO
    {
        public Guid FeedbackId { get; set; }
        public Guid EventId { get; set; }
        public Guid UserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
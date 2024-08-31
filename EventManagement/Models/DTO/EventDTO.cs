using EventManagement.Models.Domain;

namespace EventManagement.Models.DTO
{
    public class EventDTO
    {
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime MaxDate { get; set; }
        public int MaxParticipants { get; set; }
        public Guid CreatedBy { get; set; }

        // Navigation properties
        public User CreatedByUser { get; set; }
        public ICollection<EventUser> EventUsers { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
    }
}

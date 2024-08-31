using EventManagement.Models.Domain;

namespace EventManagement.Models.DTO
{
    public class EventUserDTO
    {
        public Guid EventId { get; set; }
        public Guid UserId { get; set; }
        public bool IsAdmin { get; set; }

        // Navigation properties
        public Event Event { get; set; }
        public User User { get; set; }
    }
}

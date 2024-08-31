using System.Collections.Generic;

namespace EventManagement.Models.Domain
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Navigation properties
        public ICollection<Event> EventsCreated { get; set; }
        public ICollection<EventUser> EventUsers { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
    }
}

using EventManagement.Models.Domain;

namespace EventManagement.Models.DTO
{
    public class UpdateFeedbackDTO
    {
        public Guid FeedbackId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
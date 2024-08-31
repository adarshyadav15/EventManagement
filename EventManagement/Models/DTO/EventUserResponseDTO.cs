namespace EventManagement.Models.DTO
{
    public class EventUserResponseDTO
    {
        public Guid EventId { get; set; }
        public Guid UserId { get; set; }
        public bool IsAdmin { get; set; }
        public DisplayEventDTO Event { get; set; }
        public DisplayUserDTO User { get; set; }
    }
}

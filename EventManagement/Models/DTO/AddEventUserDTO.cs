namespace EventManagement.Models.DTO
{
    public class AddEventUserDTO
    {
        public Guid EventId { get; set; }
        public Guid UserId { get; set; }
        public bool IsAdmin { get; set; }
    }
}

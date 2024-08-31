using EventManagement.Models.Domain;
using EventManagement.Models.DTO;

namespace EventManagement.Repository
{
    public interface IEventUserRepository
    {
        Task<bool> EventExists(Guid eventId);
        Task<bool> UserExists(Guid userId);
        Task<bool> EventUserExists(Guid eventId, Guid userId);
        Task AddEventUser(EventUser eventUser);
        Task<List<EventUserResponseDTO>> GetEventUsers(Guid eventId);
        Task<int> GetEventUserCount(Guid eventId);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using EventManagement.Models.Domain;

namespace EventManagement.Repository
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetEventsAsync(string sortBy = "MaxDate", bool ascending = true);
        Task<Event> GetEventByIdAsync(Guid eventId);
        Task<Event> CreateEventAsync(Event newEvent);
        Task<Event> UpdateEventAsync(Event updatedEvent);
        Task<int> DeleteEventAsync(Guid eventId);
        Task<IEnumerable<Event>> GetEventsCreatedByUserAsync(Guid userId);


    }
}
using Dapper;
using EventManagement.Models.Domain;
using EventManagement.Models.DTO;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace EventManagement.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly IDbConnection _connection;

        public EventRepository(IDbConnection connection)
        {
            _connection = connection;
        }


        public async Task<IEnumerable<Event>> GetEventsAsync(string sortBy = "MaxDate", bool ascending = true)
        {
                var sql = $"SELECT * FROM Events ORDER BY {sortBy} {(ascending ? "ASC" : "DESC")}";
                return await _connection.QueryAsync<Event>(sql);
        }


        public async Task<Event> GetEventByIdAsync(Guid eventId)
        {
            var sql = "SELECT * FROM Events WHERE EventId = @EventId";
            return await _connection.QuerySingleOrDefaultAsync<Event>(sql, new { EventId = eventId });
        }


        public async Task<Event> CreateEventAsync(Event newEvent)
        {
            var sql = @"
        INSERT INTO Events (Name, Description, Location, MaxDate, MaxParticipants, CreatedBy)
        OUTPUT inserted.*
        VALUES (@Name, @Description, @Location, @MaxDate, @MaxParticipants, @CreatedBy)";
            var eventcreated = await _connection.QueryFirstOrDefaultAsync<Event>(sql, newEvent);
            return eventcreated;
        }

        public async Task<Event> UpdateEventAsync(Event updatedEvent)
        {
            var sql = "UPDATE Events SET Name = @Name, Description = @Description, Location = @Location, MaxDate=@MaxDate, MaxParticipants=@MaxParticipants WHERE EventId = @EventId";
            await _connection.ExecuteAsync(sql, updatedEvent);
            return updatedEvent;
        }

        public async Task<int> DeleteEventAsync(Guid eventId)
        {
            var sql = "DELETE FROM Events WHERE EventId = @EventId";
            return await _connection.ExecuteAsync(sql, new { EventId = eventId });
        }
        public async Task<IEnumerable<Event>> GetEventsCreatedByUserAsync(Guid userId)
        {
            var sql = "SELECT * FROM Events WHERE CreatedBy = @UserId";
            return await _connection.QueryAsync<Event>(sql, new { UserId = userId });
        }


    }
}
using Dapper;
using EventManagement.Models.Domain;
using EventManagement.Models.DTO;
using System.Data;

namespace EventManagement.Repository
{
    public class EventUserRepository : IEventUserRepository
    {
        private readonly IDbConnection _connection;

        public EventUserRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task AddEventUser(EventUser eventUser)
        {
            var sql = "INSERT INTO EventUser (EventId, UserId, IsAdmin) VALUES (@EventId, @UserId, @IsAdmin)";
            await _connection.ExecuteAsync(sql, eventUser);
        }

        public async Task<bool> EventExists(Guid eventId)
        {
            var sql = "SELECT COUNT(*) FROM Events WHERE EventId = @EventId";
            var count = await _connection.ExecuteScalarAsync<int>(sql, new { EventId = eventId });
            return count > 0;
        }
        public async Task<bool> UserExists(Guid userId)
        {
            var sql = "SELECT COUNT(*) FROM Users WHERE UserId = @UserId";
            var count = await _connection.ExecuteScalarAsync<int>(sql, new { UserId = userId });
            return count > 0;
        }

        public async Task<bool> EventUserExists(Guid eventId, Guid userId)
        {
            var sql = "SELECT COUNT(*) FROM EventUser WHERE EventId = @EventId AND UserId = @UserId";
            var count = await _connection.ExecuteScalarAsync<int>(sql, new { EventId = eventId, UserId = userId });
            return count > 0;
        }

        public async Task<List<EventUserResponseDTO>> GetEventUsers(Guid eventId)
        {
            var sql = @"
        SELECT 
            eu.EventId, 
            eu.UserId, 
            eu.IsAdmin, 
            e.EventId, 
            e.Name, 
            e.Description, 
            e.Location, 
            e.MaxDate, 
            e.MaxParticipants, 
            e.CreatedBy,
            u.UserId, 
            u.Name, 
            u.Email
        FROM 
            EventUser eu
        JOIN 
            Events e ON eu.EventId = e.EventId
        JOIN 
            Users u ON eu.UserId = u.UserId
        WHERE 
            eu.EventId = @EventId";

            return (await _connection.QueryAsync<EventUserResponseDTO, DisplayEventDTO, DisplayUserDTO, EventUserResponseDTO>(
                sql,
                (eventUserResponseDto, e, u) => {
                    eventUserResponseDto.Event = e;
                    eventUserResponseDto.User = u;
                    return eventUserResponseDto;
                },
                new { EventId = eventId },
                splitOn: "EventId, UserId"
            )).ToList();
        }

        public async Task<int> GetEventUserCount(Guid eventId)
        {
            var sql = "SELECT COUNT(*) FROM EventUser WHERE EventId = @EventId";
            var count = await _connection.ExecuteScalarAsync<int>(sql, new { EventId = eventId });
            return count;
        }
    }
}

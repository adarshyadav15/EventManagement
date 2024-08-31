using Dapper;
using EventManagement.Models.Domain;
using EventManagement.Models.DTO;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace EventManagement.Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly IDbConnection _connection;

        public FeedbackRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Feedback>> GetFeedbacksAsync()
        {
            var sql = "SELECT FeedbackId, EventId, UserId, Rating, Comment FROM Feedback";
            return await _connection.QueryAsync<Feedback>(sql);
        }

        public async Task<Feedback> GetFeedbackByIdAsync(Guid feedbackId)
        {
            var sql = "SELECT FeedbackId, EventId, UserId, Rating, Comment FROM Feedback WHERE FeedbackId = @FeedbackId";
            return await _connection.QuerySingleOrDefaultAsync<Feedback>(sql, new { FeedbackId = feedbackId });
        }

        public async Task<Feedback> CreateFeedbackAsync(Feedback newFeedback)
        {
            var sql = "INSERT INTO Feedback (EventId, UserId, Rating, Comment) OUTPUT inserted.* VALUES (@EventId, @UserId, @Rating, @Comment)";
            var insertedFeedback = await _connection.QueryFirstOrDefaultAsync<Feedback>(sql, newFeedback);
            return insertedFeedback;
        }

        public async Task<Feedback> UpdateFeedbackAsync(Feedback updatedFeedback)
        {
            var sql = "UPDATE Feedback SET Rating = @Rating, Comment = @Comment WHERE FeedbackId = @FeedbackId";
            await _connection.ExecuteAsync(sql, updatedFeedback);
            return updatedFeedback;
        }

        public async Task<int> DeleteFeedbackAsync(Guid feedbackId)
        {
            var sql = "DELETE FROM Feedback WHERE FeedbackId = @FeedbackId";
            return await _connection.ExecuteAsync(sql, new { FeedbackId = feedbackId });
        }
    }
}
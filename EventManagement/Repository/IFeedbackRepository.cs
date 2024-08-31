using EventManagement.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManagement.Repository
{
    public interface IFeedbackRepository
    {
        Task<IEnumerable<Feedback>> GetFeedbacksAsync();
        Task<Feedback> GetFeedbackByIdAsync(Guid feedbackId);
        Task<Feedback> CreateFeedbackAsync(Feedback newFeedback);
        Task<Feedback> UpdateFeedbackAsync(Feedback updatedFeedback);
        Task<int> DeleteFeedbackAsync(Guid feedbackId);
    }
}
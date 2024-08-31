using System.Collections.Generic;
using System.Threading.Tasks;
using EventManagement.Models.Domain;

namespace EventManagement.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(Guid userId);
        Task<User> CreateUserAsync(User newUser);
        Task<User> UpdateUserAsync(User updatedUser);
        Task<int> DeleteUserAsync(Guid userId);
        Task<User> GetUserByEmailAsync(string email);
    }
}
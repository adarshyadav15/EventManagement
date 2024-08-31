using Dapper;
using EventManagement.Models.Domain;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace EventManagement.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _connection;

        public UserRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var sql = "SELECT * FROM Users";
            return await _connection.QueryAsync<User>(sql);
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            var sql = "SELECT * FROM Users WHERE UserId = @UserId";
            return await _connection.QuerySingleOrDefaultAsync<User>(sql, new { UserId = userId });
        }

        public async Task<User> CreateUserAsync(User newUser)
        {
            var sql = "INSERT INTO Users (Name, Email, Password) OUTPUT inserted.* VALUES (@Name, @Email, @Password)";
            var insertedUser = await _connection.QueryFirstOrDefaultAsync<User>(sql, newUser);
            return insertedUser;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var sql = "UPDATE Users SET Name = @Name, Email = @Email, Password = @Password WHERE UserId = @UserId";
            await _connection.ExecuteAsync(sql, user);
            return user; 
        }

        public async Task<int> DeleteUserAsync(Guid userId)
        {
            var sql = "DELETE FROM Users WHERE UserId = @UserId";
            return await _connection.ExecuteAsync(sql, new { UserId = userId });
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var sql = "SELECT * FROM Users WHERE Email = @Email";
            return await _connection.QueryFirstOrDefaultAsync<User>(sql, new { Email = email });
        }

    }
}
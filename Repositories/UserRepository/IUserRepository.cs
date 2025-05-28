using System;
using TodoApi.Model;

namespace TodoApi.Repositories.UserRepository;

public interface IUserRepository
{
    Task<User> GetUserByIdAsync(int userId);
    Task<User> GetUserByEmailAsync(string email);
    Task<User> CreateUserAsync(User user);
    Task<User> GetUserByNameAsync(string name);
}

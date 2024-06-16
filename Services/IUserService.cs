using DTOs;
using Entities;

namespace Services
{
    public interface IUserService
    {
        int CheckPassword(string password);
        Task<User> GetById(int id);
        Task<User> Login(User user);
        Task<User> Register(User user);
        Task<User> UpdateUser(int id, User user);
    }
}
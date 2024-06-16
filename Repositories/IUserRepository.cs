using Entities;

namespace Repositories
{
    public interface IUserRepository
    {
        Task<User> GetById(int id);
        Task<User> Login(User userLogin);
        Task<User> Register(User user);
        Task<User> UpdateUser(int id, User user);
    }
}
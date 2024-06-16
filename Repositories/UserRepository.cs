using Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;


namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private AdoNetMarketContext _AdoNetMarket;

        public UserRepository(AdoNetMarketContext AdoNetMarket)
        {
            _AdoNetMarket = AdoNetMarket;
        }
        public async Task<User> GetById(int id)

        {
            try
            {
                return await _AdoNetMarket.Users.FindAsync(id);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<User>  Register(User user)
        {
                await _AdoNetMarket.Users.AddAsync(user);
                await _AdoNetMarket.SaveChangesAsync();
                return user;
        }

        public async Task<User> Login(User userLogin)
        {
     
                return await _AdoNetMarket.Users.Where(u => u.Email == userLogin.Email && u.Password == userLogin.Password).FirstOrDefaultAsync();
        }



        public async Task<User> UpdateUser(int id, User user)
        {
            user.UserId = id;
                 _AdoNetMarket.Users.Update(user);
            await _AdoNetMarket.SaveChangesAsync();
            return user;

        }
    }
}

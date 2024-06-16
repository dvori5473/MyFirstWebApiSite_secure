using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyFirstWebApiSite;
using Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Zxcvbn;
namespace Services
{

    public class UserService : IUserService
    {



        //UserRepository userRepositories = new UserRepository();
        private IUserRepository _userRepository;
        private IConfiguration _configuration;
        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<User> GetById(int id)
        {

            return await _userRepository.GetById(id);
        }
        public async Task<User> Login(User userLogin)
        {
            var user = await _userRepository.Login(userLogin); ;
            user.Token = generateJwtToken(user);
            return user;
        }
        public int CheckPassword(string password)
        {
            var result = Zxcvbn.Core.EvaluatePassword(password);
            return result.Score;
        }
        public async Task<User> Register(User user)
        {
            if (CheckPassword(user.Password) > 1)
                return await _userRepository.Register(user);
            return null;

        }
        public async Task<User> UpdateUser(int id, User user)
        {

            if (CheckPassword(user.Password) > 1)
                return await _userRepository.UpdateUser(id, user);
            return null;
        }

        private string generateJwtToken(User user)
        {
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("key").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                   // new Claim("roleId", 7.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }


    }
}

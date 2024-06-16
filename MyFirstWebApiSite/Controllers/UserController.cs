using AutoMapper;
using DTOs;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Text.Json;

namespace MyFirstWebApiSite.Controllers
{
   
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        //UserService userService = new UserService();
        private IUserService _userService;
        private IMapper _mapper;
        private readonly ILogger<UserController> _Ilogger;
        public UserController(IUserService userService, IMapper mapper,ILogger<UserController> Ilogger)
        {
            _userService = userService;
            _mapper = mapper;
            _Ilogger = Ilogger;
        }
        [HttpGet]
        public async Task<ActionResult<User>> GetById(int id)
            
        {    try{
                return await _userService.GetById(id);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<User>> Register([FromBody] UserDto user)
        {
            try {
                User regolarUser = _mapper.Map<UserDto, User>(user);
                User user1 = await _userService.Register(regolarUser);
                if (user1 != null)
                    return Ok(user1);
                return Unauthorized();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [Route("CheckPassword")]
        public ActionResult<int> CheckPassword([FromBody] string password)
        {
            try
            {
                return Ok(_userService.CheckPassword(password));
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<User>> Login([FromBody] UserLoginDto userLogin)
        {
            try {
                User regolarUser1 = _mapper.Map<UserLoginDto, User>(userLogin);
                _Ilogger.LogInformation($"login attempted with User name ,{regolarUser1.Email} and password {regolarUser1.Password}");
                User user1 = await _userService.Login(regolarUser1);
                if (user1 != null)
                {
                    Response.Cookies.Append("X-Access-Token", user1.Token, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                    return Ok(user1);
                }      
               return NoContent();
        
            } catch(Exception ex)
            {
                throw ex;
            }

}
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> UpdateUser(int id,[FromBody] UserDto user)
        {
            try {
                User regolarUser = _mapper.Map<UserDto, User>(user);
                User user1 =await  _userService.UpdateUser(id, regolarUser);
                if (user1 != null)
                     return Ok(user1);
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}

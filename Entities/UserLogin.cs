using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class UserLogin
    {
        public string Password { get; set; }
        [EmailAddress]
        public string UserName { get; set; }
    }
}

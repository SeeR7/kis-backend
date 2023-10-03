using Microsoft.AspNetCore.Identity;

namespace ServerApp.Models.DBO
{
    public class UserAuth
    {
        public required string Login { get; set; }
        public required string Password { get; set; }
    }
}

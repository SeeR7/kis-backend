

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models.Local
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? AccessGroup { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

    }
}

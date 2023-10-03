using Microsoft.AspNetCore.Mvc;
using ServerApp.Data;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using ServerApp.Models.Local;
using ServerApp.Models.DBO;

namespace ServerApp.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ApiDbContext _context;

        public AuthController(IConfiguration configuration, ApiDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserAuth userAuth)
        {
            var user = await _context.User.SingleOrDefaultAsync(e => e.Login == userAuth.Login && e.Password == userAuth.Password);

            if (user == null)
            {
                return BadRequest("Wrong login or password");
            }

            string token = CreateToken(user);
            var refreshToken = GenerateRefreshToken();
            _ = SetRefreshToken(refreshToken, user);

            return Ok(token);
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var id =  Request.Cookies["id"];
            if (id == null)
            {
                return Unauthorized("Invalid UserId. ");
            }
            var user = await _context.User.SingleOrDefaultAsync(e => e.Id == int.Parse(id));
            if (user == null)
            {
                return Unauthorized("Invalid Username. ");
            }
            if (user.RefreshToken == null || !user.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token.");
            }
            else if (user.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                return Unauthorized("Token expired.");
            }

            string token = CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();

            _ = SetRefreshToken(newRefreshToken, user);

            return Ok(token);
        }

        
        [HttpGet]
        [Route("get-me")]
        public async Task<IActionResult> GetMe()
        {
            var id = Request.Cookies["id"];
            if (id == null)
            {
                return Unauthorized("Invalid UserId.");
            }
            var employee = await _context.Employee.Include("Department").Include("User").SingleOrDefaultAsync(e => e.UserId == int.Parse(id));
            if (employee == null)
            {
                return Unauthorized("Invalid Employee. ");
            }
            //var department = await _context.Department.SingleOrDefaultAsync(e => e.Id == employee.DepartmentId);

            return Ok(employee); // Как вернуть один объект содержащий и работника и цех? return Ok( new {employee, department});
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var id = Request.Cookies["id"];
            if (id == null)
            {
                return Unauthorized("Invalid UserId.");
            }
            var user = await _context.User.SingleOrDefaultAsync(e => e.Id == int.Parse(id));
            if (user == null)
            {
                Response.Cookies.Delete("refreshToken");
                Response.Cookies.Delete("id");
                return Unauthorized("Invalid Username. ");
            }
            if (user.RefreshToken == null || !user.RefreshToken.Equals(refreshToken))
            {
                Response.Cookies.Delete("refreshToken");
                Response.Cookies.Delete("id");
                return Unauthorized("Invalid Refresh Token.");
            }
            else if (user.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                Response.Cookies.Delete("refreshToken");
                Response.Cookies.Delete("id");
                return Unauthorized("Token expired.");
            }

            user.RefreshToken = null;
            await _context.SaveChangesAsync();
            Response.Cookies.Delete("refreshToken");
            Response.Cookies.Delete("id");

            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            var user = await _context.User.SingleOrDefaultAsync(e => username == e.Login);
            if (user == null)
            {
                return BadRequest("Invalid username");
            }

            user.RefreshToken = null;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [Authorize]
        [HttpPost]
        [Route("revoke-all")]
        public async Task<IActionResult> RevokeAll()
        {
            var users = _context.User.ToList();
            foreach (var user in users)
            {
                user.RefreshToken = null;
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private string CreateToken(User user)
        {
            var secret = _configuration.GetSection("JWT:SecretKey").Value;
            if (user.AccessGroup == null || secret == null)
            {
                return "";
            }
            var employee = _context.Employee.Include("Department").Where(u => u.UserId == user.Id).FirstOrDefault();
            List<Claim> claims = new List<Claim>
            {
                new Claim("name", user.Id.ToString()),
                new Claim("role", user.AccessGroup),
                new Claim("dep", employee.Department.Number.ToString()),
                new Claim("fio", employee.LastName + " " + employee.FirstName[0] + "." + employee.MiddleName[0] + ".")
            };
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                audience: _configuration["JWT:ValidAudience"],
                issuer: _configuration["JWT:ValidIssuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(tokenValidityInMinutes),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private RefreshToken GenerateRefreshToken()
        {
            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.UtcNow.AddDays(refreshTokenValidityInDays)
            };
            return refreshToken;
        }

        private async Task<IActionResult> SetRefreshToken(RefreshToken newRefreshToken, User user)
        {
            if (newRefreshToken.Token == null)
            {
                return BadRequest("Unexpected error");
            }
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires,
                SameSite = SameSiteMode.Lax,
                Secure = false
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
            Response.Cookies.Append("id", user.Id.ToString(), cookieOptions);
            user.RefreshToken = newRefreshToken.Token;
            user.RefreshTokenExpiryTime = newRefreshToken.Expires;

            await _context.SaveChangesAsync();
            return Ok();
        }

        
    }
}

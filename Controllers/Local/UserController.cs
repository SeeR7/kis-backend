using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerApp.Data;
using ServerApp.Models.Local;

namespace ServerApp.Controllers.Local
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApiDbContext _apiDbContext;

        public UserController(ApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var user = await _apiDbContext.User.FindAsync(id);
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var users = await _apiDbContext.User.ToListAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(User user)
        {
            _apiDbContext.User.Add(user);
            await _apiDbContext.SaveChangesAsync();
            return Created($"/api/[controller]?id={user.Id}", user);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(User user)
        {
            _apiDbContext.User.Update(user);
            await _apiDbContext.SaveChangesAsync();
            return NoContent();
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var user = await _apiDbContext.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _apiDbContext.User.Remove(user);
            await _apiDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
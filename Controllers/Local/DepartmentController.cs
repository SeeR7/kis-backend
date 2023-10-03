using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerApp.Data;
using ServerApp.Models.Local;

namespace ServerApp.Controllers.Local
{
    //[Authorize(Roles = "Admin, Developer")]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ApiDbContext _apiDbContext;

        public DepartmentController(ApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var department = await _apiDbContext.Department.FindAsync(id);
            return Ok(department);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var departments = await _apiDbContext.Department.ToListAsync();
            return Ok(departments.OrderBy(u => u.Number));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Department department)
        {
            _apiDbContext.Department.Add(department);
            await _apiDbContext.SaveChangesAsync();
            return Created($"/api/[controller]?id={department.Id}", department);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Department department)
        {
            if (_apiDbContext.Department.Any(e => department.Id == e.Id))
            {
                _apiDbContext.Department.Update(department);
            }
            else
            {
                await PostAsync(department);
            }
            
            await _apiDbContext.SaveChangesAsync();
            return NoContent();
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var department = await _apiDbContext.Department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            _apiDbContext.Department.Remove(department);
            await _apiDbContext.SaveChangesAsync();
            return NoContent();
        }

    }
}

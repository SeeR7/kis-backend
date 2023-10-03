using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerApp.Data;
using ServerApp.Models.Local;

namespace ServerApp.Controllers.Local
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApiDbContext _apiDbContext;

        public EmployeeController(ApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var employee = await _apiDbContext.Employee.Include("Department").Include("User").FirstOrDefaultAsync(b => b.Id == id);
            return Ok(employee);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var employees = await _apiDbContext.Employee.Include("Department").Include("User").ToListAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Employee employee)
        {
            employee.BirthDate = DateTime.SpecifyKind(employee.BirthDate, DateTimeKind.Utc);
            employee.JoinDate = DateTime.SpecifyKind(employee.JoinDate, DateTimeKind.Utc);
            _apiDbContext.Employee.Add(employee);
            await _apiDbContext.SaveChangesAsync();
            return Created($"/api/[controller]?id={employee.Id}", employee);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Employee employee)
        {
            employee.BirthDate = DateTime.SpecifyKind(employee.BirthDate, DateTimeKind.Utc);
            employee.JoinDate = DateTime.SpecifyKind(employee.JoinDate, DateTimeKind.Utc);
            _apiDbContext.Employee.Update(employee);
            await _apiDbContext.SaveChangesAsync();
            return NoContent();
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var employee = await _apiDbContext.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            var user = await _apiDbContext.User.FindAsync(employee.UserId);


            _apiDbContext.Employee.Remove(employee);
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

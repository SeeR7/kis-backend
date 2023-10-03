using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerApp.Data;
using ServerApp.Models.Local;

namespace ServerApp.Controllers.Foreign
{
    
    [ApiController]
    public class ForeignController : ControllerBase
    {
        private readonly RusagrDbContext _rusagrDbContext;
        private readonly _1cDbContext _1cDbContext1;
        private readonly ApiDbContext _apiDbContext;

        public ForeignController(RusagrDbContext rusagrDbContext, _1cDbContext _1cDbC, ApiDbContext apiDbContext)
        {
            _rusagrDbContext = rusagrDbContext;
            _1cDbContext1 = _1cDbC;
            _apiDbContext = apiDbContext;   
        }

        [HttpGet("api/sostav/{id}")]
        public async Task<IActionResult> GetSostavAsync(long id)
        {
            var sostav = _rusagrDbContext.DseSostav.Include("Child").Where(u => u.AgregatId == id);
            var mySostav = new List<Object>();
            foreach(var spec in sostav)
            {
                var dse = await _1cDbContext1.DSE.FindAsync(spec.ChildId);
                var local = await _apiDbContext.DSE.FindAsync(spec.ChildId);
                var tech = _rusagrDbContext.DepRoute.Where(u => u.DseId == spec.ChildId);
                var techLocal = _apiDbContext.Technology
                    .Where(
                            u => u.DseId == spec.ChildId && u.TechCompletionPercentage == 100
                    );
                var tp = new { techLocal, tech };
 
                mySostav.Add(new { spec, dse, local, tp});
            }
            return Ok(mySostav);
        }

        [Route("api/spec")]
        [HttpGet]
        public async Task<IActionResult> GetSpecAsync()
        {
            var spec = await _rusagrDbContext.DSE.ToListAsync();
            return Ok(spec);
        }
        [Route("api/project/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetProjectAsync(int id)
        {
            var project = await _1cDbContext1.Project.FindAsync(id);
            return Ok(project);
        }
        [Route("api/project")]
        [HttpGet]
        public async Task<IActionResult> GetProjectsAsync()
        {
            var project = await _1cDbContext1.Project.ToListAsync();
            return Ok(project);
        }
        [Route("api/project-agregats/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetProjectAgregatsAsync(int id)
        {
            var project = _1cDbContext1.ProjectAgregat.Where(u => u.ProjectId == id);
            return Ok(project);
        }
        
        [Route("api/dse-card/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetDseCardAsync(long id)
        {
            var _1c = await _1cDbContext1.DSE.FindAsync(id);
            var rusagr = await _rusagrDbContext.DSE.FindAsync(id);
            var local = await _apiDbContext.DSE.FindAsync(id);
            
            var depRoute = _rusagrDbContext.DepRoute.Where(u => u.DseId == id);

            var tech = new List<Object>();
            foreach (var techRusagr in depRoute)
            {
                var techLocal = await _apiDbContext.Technology.FindAsync(techRusagr.Id);

                tech.Add(new { techRusagr, techLocal });
            }

            return Ok(new {_1c, rusagr, local, tech });
        }

        [Route("api/agregats")]
        [HttpGet]
        public async Task<IActionResult> GetAgregatsAsync()
        {
            var project = _rusagrDbContext.DSE.Where(u => u.ZagType == "А");
            return Ok(project);
        }

        [Route("api/dses")]
        [HttpGet]
        public async Task<IActionResult> GetDsesAsync()
        {
            var project = await _rusagrDbContext.DSE.ToListAsync();
            return Ok(project);
        }

        [Route("api/dse")]
        [HttpPost]
        public async Task<IActionResult> PostLocalDseAsync(DSE dse)
        {
            if (dse.PlanMechDepData != null)
            {
                dse.PlanMechDepData = DateTime.SpecifyKind((DateTime)dse.PlanMechDepData, DateTimeKind.Utc);
            }
            if (dse.PlanProdDepData != null)
            {
                dse.PlanProdDepData = DateTime.SpecifyKind((DateTime)dse.PlanProdDepData, DateTimeKind.Utc);
            }
            
            _apiDbContext.DSE.Add(dse);
            await _apiDbContext.SaveChangesAsync();
            return Created($"/api/[controller]?id={dse.Id}", dse);
        }
        [Route("api/dse")]
        [HttpPut]
        public async Task<IActionResult> UpdateLocalDseAsync(DSE dse)
        {
            if (dse.PlanMechDepData != null)
            {
                dse.PlanMechDepData = DateTime.SpecifyKind((DateTime)dse.PlanMechDepData, DateTimeKind.Utc);
            }
            if (dse.PlanProdDepData != null)
            {
                dse.PlanProdDepData = DateTime.SpecifyKind((DateTime)dse.PlanProdDepData, DateTimeKind.Utc);
            }

            if (_apiDbContext.DSE.Any(e => dse.Id == e.Id))
            {
                _apiDbContext.DSE.Update(dse);
            }
            else
            {
                await PostLocalDseAsync(dse);
            }

            await _apiDbContext.SaveChangesAsync();
            return NoContent();
        }

        [Route("api/technology")]
        [HttpPost]
        public async Task<IActionResult> PostLocalTechAsync(Technology tech)
        {
            if (tech.TechDate != null)
            {
                tech.TechDate = DateTime.SpecifyKind((DateTime)tech.TechDate, DateTimeKind.Utc);
            }

            _apiDbContext.Technology.Add(tech);
            await _apiDbContext.SaveChangesAsync();
            return Created($"/api/[controller]?id={tech.Id}", tech);
        }
        [Route("api/technology")]
        [HttpPut]
        public async Task<IActionResult> UpdateLocalTechAsync(Technology tech)
        {
            if (tech.TechDate != null)
            {
                tech.TechDate = DateTime.SpecifyKind((DateTime)tech.TechDate, DateTimeKind.Utc);
            }


            if (_apiDbContext.Technology.Any(e => tech.Id == e.Id))
            {
                _apiDbContext.Technology.Update(tech);
            }
            else
            {
                await PostLocalTechAsync(tech);
            }

            await _apiDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}

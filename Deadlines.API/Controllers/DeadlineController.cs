using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Deadlines.API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Deadlines.API.Controllers
{
    //[Authorize()]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class DeadlineController : ControllerBase
    {
        private readonly EfContext _context;
        
        public DeadlineController(EfContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Model.Deadline>>> GetAll()
        {
            Console.WriteLine("Is auth: " + User.Identity.IsAuthenticated);

            var userId = User.FindFirst("id");
            
            var deadlines = _context.Deadlines.Select(d => new Model.Deadline()
            {
                Id = d.Id,
                Title = d.Title,
                Description = d.Description,
                StartDate = d.StartDate,
                EndDate = d.EndDate,
                DateCreate = d.DateCreate,
            });
            return new OkObjectResult(deadlines);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Model.Deadline>> Get(int id)
        {
            var d = await _context.Deadlines.FindAsync(id);
            
            if (d == null) return NotFound();
            
            var deadline = new Model.Deadline() 
            {
                Id = d.Id,
                Title = d.Title,
                Description = d.Description,
                StartDate = d.StartDate,
                EndDate = d.EndDate,
                DateCreate = d.DateCreate,
            };
            
            return new OkObjectResult(deadline);
        }


        [HttpPost]
        public async Task<ActionResult<Model.Deadline>> Post(Model.Deadline d)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var userId = int.Parse(User.FindFirst("id").Value);

            
            var deadline = new Deadline()
            {
                Id = d.Id,
                Title = d.Title,
                DbUserId = userId,
                Description = d.Description,
                StartDate = d.StartDate,
                EndDate = d.EndDate,
                DateCreate = DateTime.Now,
            };
            
            _context.Deadlines.Add(deadline);
            await _context.SaveChangesAsync();
        
            return CreatedAtAction("Get", new {id = deadline.Id}, deadline);
        }
    }
}   
using LimpopoTourDestinations.Data;
using LimpopoTourDestinations.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LimpopoTourDestinations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuideController : ControllerBase
    {
        private readonly TourDbContext _context;

        public GuideController(TourDbContext context)
        {
            _context = context;
        }

        // GET: api/Guide
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // FIXED: was fetching Tours instead of Guides
            var guides = await _context.Guides
                .Include(g => g.Tours)
                .ToListAsync();
            return Ok(guides);
        }

        // GET: api/Guide/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var guide = await _context.Guides
                .Include(g => g.Tours)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (guide == null)
                return NotFound("Guide not found");

            return Ok(guide);
        }

        // POST: api/Guide
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Guide guide)
        {
            if (guide == null)
                return BadRequest("Guide cannot be empty");

            // FIXED: validate against actual model properties
            if (string.IsNullOrWhiteSpace(guide.FullName))
                return BadRequest("Guide full name is required");
            if (string.IsNullOrWhiteSpace(guide.Language))
                return BadRequest("Guide language is required");
            if (string.IsNullOrWhiteSpace(guide.Bio))
                return BadRequest("Guide bio is required");

            guide.Id = Guid.NewGuid();
            guide.Tours = new List<Tour>();

            _context.Guides.Add(guide);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = guide.Id }, guide);
        }

        // PUT: api/Guide/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Guide updatedGuide)
        {
            if (updatedGuide == null)
                return BadRequest("Invalid guide data");

            var existingGuide = await _context.Guides.FindAsync(id);

            if (existingGuide == null)
                return NotFound("Guide not found");

            // FIXED: update only fields that exist on the model
            existingGuide.FullName = updatedGuide.FullName;
            existingGuide.Language = updatedGuide.Language;
            existingGuide.Bio = updatedGuide.Bio;

            await _context.SaveChangesAsync();
            return Ok(existingGuide);
        }

        // DELETE: api/Guide/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var guide = await _context.Guides
                .Include(g => g.Tours)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (guide == null)
                return NotFound("Guide not found");

            // Prevent deleting a guide who still has tours assigned
            if (guide.Tours.Any())
                return BadRequest("Cannot delete a guide who still has tours assigned");

            _context.Guides.Remove(guide);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
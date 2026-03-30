using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LimpopoTourDestinations.Data;
using LimpopoTourDestinations.Models.Domain;

namespace LimpopoTourDestinations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourController : ControllerBase
    {
        private readonly TourDbContext _context;

        public TourController(TourDbContext context)
        {
            _context = context;
        }

        // GET: api/Tour
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tours = await _context.Tours
                .Include(t => t.Bookings)
                .ToListAsync();
            return Ok(tours);
        }

        // GET: api/Tour/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var tour = await _context.Tours
                .Include(t => t.Bookings)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tour == null)
                return NotFound("Tour not found");

            return Ok(tour);
        }

        // POST: api/Tour
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Tour tour)
        {
            if (tour == null)
                return BadRequest("Tour cannot be empty");

            // Validate required fields
            if (string.IsNullOrWhiteSpace(tour.Name))
                return BadRequest("Tour name is required");
            if (string.IsNullOrWhiteSpace(tour.Description))
                return BadRequest("Tour description is required");
            if (string.IsNullOrWhiteSpace(tour.Location))
                return BadRequest("Tour location is required");
            if (tour.Price <= 0)
                return BadRequest("Price must be greater than zero");
            if (tour.DurationDays <= 0)
                return BadRequest("DurationDays must be greater than zero");

            // Only check guide exists if GuideId is provided
            if (tour.GuideId.HasValue)
            {
                var guideExists = await _context.Guides.AnyAsync(g => g.Id == tour.GuideId.Value);
                if (!guideExists)
                {
                    // Instead of rejecting, set GuideId to null if invalid
                    tour.GuideId = null;
                }
            }

            // Assign new Id if not provided
            if (tour.Id == Guid.Empty)
                tour.Id = Guid.NewGuid();

            // Initialize bookings list if null
            if (tour.Bookings == null)
                tour.Bookings = new List<Booking>();

            await _context.Tours.AddAsync(tour);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = tour.Id }, tour);
        }

        // PUT: api/Tour/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Tour updatedTour)
        {
            if (updatedTour == null)
                return BadRequest("Invalid tour data");

            // Find the existing tour by URL id
            var existingTour = await _context.Tours
                .Include(t => t.Bookings)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (existingTour == null)
                return NotFound("Tour not found");

            // Update main tour fields
            existingTour.Name = updatedTour.Name;
            existingTour.Description = updatedTour.Description;
            existingTour.Price = updatedTour.Price;
            existingTour.DurationDays = updatedTour.DurationDays;
            existingTour.Location = updatedTour.Location;
            existingTour.TourImageurl = updatedTour.TourImageurl;
            existingTour.GuideId = updatedTour.GuideId;

            // Handle bookings safely
            if (updatedTour.Bookings != null)
            {
                foreach (var booking in updatedTour.Bookings)
                {
                    // Generate new ID for any booking with Guid.Empty
                    if (booking.Id == Guid.Empty)
                        booking.Id = Guid.NewGuid();
                }

                // Replace existing bookings with the updated ones
                existingTour.Bookings = updatedTour.Bookings;
            }

            await _context.SaveChangesAsync();

            return Ok(existingTour);
        }

        // DELETE: api/Tour/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var tour = await _context.Tours
                .Include(t => t.Bookings)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tour == null)
                return NotFound("Tour not found");

            if (tour.Bookings.Any())
                return BadRequest("Cannot delete tour with bookings");

            _context.Tours.Remove(tour);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
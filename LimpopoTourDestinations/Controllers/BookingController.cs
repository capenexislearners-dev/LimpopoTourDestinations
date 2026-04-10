using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LimpopoTourDestinations.Data;
using LimpopoTourDestinations.Models.Domain;

namespace LimpopoTourDestinations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly TourDbContext _context;

        public BookingController(TourDbContext context)
        {
            _context = context;
        }

        // POST: api/Booking
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Booking booking)
        {
            if (booking == null)
                return BadRequest("Booking cannot be empty");

            // Validate TourId
            if (booking.TourId == Guid.Empty)
                return BadRequest("TourId is required");

            var tourExists = await _context.Tours
                .AnyAsync(t => t.GuideId  == booking.TourId);
            
            if (!tourExists)
                return BadRequest("Invalid TourId");

            // Assign values
            booking.Id = Guid.NewGuid();
            booking.BookedAt = DateTime.UtcNow;

            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();

            return Ok(booking);
        }

        // GET: api/Booking
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookings = await _context.Bookings
                .Include(b => b.Tour)
                .Select(b => new
                {
                    b.Id,
                    b.CustomerName,
                    b.CustomerEmail,
                    b.BookedAt,
                    b.NumberOfPeople,
                    TourName = b.Tour != null ? b.Tour.Name : "No Tour"
                })
                .ToListAsync();

            return Ok(bookings);
        }

        // GET: api/Booking/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Tour)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
                return NotFound("Booking not found");

            return Ok(new
            {
                booking.Id,
                booking.CustomerName,
                booking.CustomerEmail,
                booking.BookedAt,
                booking.NumberOfPeople,
                TourName = booking.Tour != null ? booking.Tour.Name : "No Tour"
            });
        }

        // DELETE: api/Booking/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
                return NotFound("Booking not found");

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
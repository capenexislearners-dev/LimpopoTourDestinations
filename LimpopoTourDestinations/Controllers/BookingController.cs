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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Booking booking)
        {
            if (booking == null)
                return BadRequest("Booking cannot be empty");

            booking.Id = Guid.NewGuid();
            booking.BookedAt = DateTime.UtcNow;

            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();

            return Ok(booking);
        }
    }
}
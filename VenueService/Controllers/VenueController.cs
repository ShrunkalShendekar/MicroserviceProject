using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VenueService.Data;
using VenueService.Model;


namespace VenueService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VenueController : ControllerBase
    {
        private readonly VenueDbContext _context;

        public VenueController(VenueDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venue>>> GetAll()
        {
            return await _context.Venues.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Venue>> GetById(Guid id)
        {
            var venue = await _context.Venues.FindAsync(id);
            if (venue == null)
                return NotFound();

            return venue;
        }

        [HttpPost]
        public async Task<ActionResult<Venue>> Create(Venue venue)
        {
            _context.Venues.Add(venue);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = venue.Id }, venue);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Venue updatedVenue)
        {
            if (id != updatedVenue.Id)
                return BadRequest();

            _context.Entry(updatedVenue).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var venue = await _context.Venues.FindAsync(id);
            if (venue == null)
                return NotFound();

            _context.Venues.Remove(venue);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

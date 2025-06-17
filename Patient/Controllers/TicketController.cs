using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketService.Application;
using TicketService.Data;

namespace TicketService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly TicketDbContext _context;

        public TicketController(IMediator mediator, TicketDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        // 1. Reserve Ticket (Creates ticket with reservation window)
        [HttpPost("reserve")]
        public async Task<IActionResult> Reserve(CreateTicketCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        // 2. Purchase Ticket
        [HttpPost("{id}/pay")]
        public async Task<IActionResult> Pay(Guid id)
        {
            var result = await _mediator.Send(new MarkTicketAsPaidCommand { TicketId = id });
            if (!result)
                return BadRequest("Payment failed or ticket already paid/cancelled/expired.");
            return Ok("Payment successful.");
        }

        // 3. Cancel Reservation
        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var result = await _mediator.Send(new CancelTicketReservationCommand { TicketId = id });
            if (!result)
                return BadRequest("Cancellation failed. Ticket may already be paid or cancelled.");
            return Ok("Ticket reservation cancelled.");
        }

        // 4. View Available Tickets for an Event
        [HttpGet("availability/{eventId}")]
        public async Task<IActionResult> GetAvailableTickets(Guid eventId)
        {
            var availableCount = await _context.Tickets
                .Where(t => t.EventId == eventId && !t.IsPaid && !t.IsCancelled && t.ExpiresAt > DateTime.UtcNow)
                .CountAsync();

            return Ok(new { Available = availableCount });
        }

        // (Optional) Get Ticket by ID - used by CreatedAtAction
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
                return NotFound();

            return Ok(ticket);
        }
    }
}

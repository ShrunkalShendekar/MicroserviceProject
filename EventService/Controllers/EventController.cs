using EventService.Application.Command;
using EventService.Application.Query;
using EventService.Data;
using EventsService;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Provider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly EventDbContext _context;


        public EventController(IMediator mediator, EventDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEventCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateEventCommand command)
        {
            if (id != command.Id)
                return BadRequest("Event ID mismatch");

            var result = await _mediator.Send(command);
            return result ? NoContent() : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var events = await _mediator.Send(new GetAllEventsQuery());
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var ev = await _mediator.Send(new GetEventByIdQuery { Id = id
            });
            return ev != null ? Ok(ev) : NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteEventCommand { Id = id });
            return result ? Ok() : NotFound();
        }
        [HttpPost("{eventId}/ticket-types")]
        public async Task<IActionResult> AddTicketType(Guid eventId, AddTicketTypeCommand command)
        {
            if (eventId != command.EventId)
                return BadRequest("Event ID mismatch");

            var id = await _mediator.Send(command);
            return Ok(id);
        }
        [HttpGet("{eventId}/capacity")]
        public async Task<IActionResult> GetRemainingCapacity(Guid eventId)
        {
            var eventEntity = await _context.Events
                .Include(e => e.TicketTypes)
                .FirstOrDefaultAsync(e => e.Id == eventId);

            if (eventEntity == null) return NotFound();

            int remaining = eventEntity.TicketTypes.Sum(t => t.Quantity);
            return Ok(new { remaining });
        }

    }
}

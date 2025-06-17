using EventService.Data;
using MediatR;

namespace EventService.Application.Command
{
    public class UpdateEventCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Venue { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
    }

    public class UpdateEventHandler : IRequestHandler<UpdateEventCommand, bool>
    {
        private readonly EventDbContext _context;

        public UpdateEventHandler(EventDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var ev = await _context.Events.FindAsync(request.Id);
            if (ev == null) return false;

            ev.Title = request.Title;
            ev.Venue = request.Venue;
            ev.Date = request.Date;
            ev.Description = request.Description;
            ev.Capacity = request.Capacity;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}

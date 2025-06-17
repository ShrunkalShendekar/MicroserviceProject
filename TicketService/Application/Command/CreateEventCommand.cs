using EventService.Data;
using EventService.NewFolder;
using MediatR;

namespace EventService.Application.Command
{
    public class CreateEventCommand : IRequest<Guid>
    {
        public string Title { get; set; }
        public string Venue { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
    }
    public class CreateEventHandler : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly EventDbContext _context;

        public CreateEventHandler(EventDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var ev = new Event
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Venue = request.Venue,
                Date = request.Date,
                Description = request.Description,
                Capacity = request.Capacity
            };

            _context.Events.Add(ev);
            await _context.SaveChangesAsync(cancellationToken);
            return ev.Id;
        }
    }

}

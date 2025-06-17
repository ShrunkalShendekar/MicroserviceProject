using EventService.Data;
using MediatR;

namespace EventService.Application.Command
{
    public class DeleteEventCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteEventHandler : IRequestHandler<DeleteEventCommand, bool>
    {
        private readonly EventDbContext _context;

        public DeleteEventHandler(EventDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var ev = await _context.Events.FindAsync(request.Id);
            if (ev == null) return false;

            _context.Events.Remove(ev);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}

using EventService.Data;
using EventService.NewFolder;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventService.Application.Query
{
    public class GetAllEventsQuery : IRequest<List<Event>> { }

    public class GetAllEventsHandler : IRequestHandler<GetAllEventsQuery, List<Event>>
    {
        private readonly EventDbContext _context;

        public GetAllEventsHandler(EventDbContext context)
        {
            _context = context;
        }

        public async Task<List<Event>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Events.ToListAsync(cancellationToken);
        }
    }
}

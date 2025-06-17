using EventService.Data;
using EventService.NewFolder;
using MediatR;

public class GetEventByIdQuery : IRequest<Event>
{
    public Guid Id { get; set; }
}

public class GetEventByIdHandler : IRequestHandler<GetEventByIdQuery, Event>
{
    private readonly EventDbContext _context;

    public GetEventByIdHandler(EventDbContext context)
    {
        _context = context;
    }

    public async Task<Event> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Events.FindAsync(request.Id);
    }
}

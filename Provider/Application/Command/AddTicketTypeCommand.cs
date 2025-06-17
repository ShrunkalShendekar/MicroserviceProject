using EventService.Data;
using EventService.Model;
using MediatR;

namespace EventService.Application.Command
{
    public class AddTicketTypeCommand : IRequest<Guid>
    {
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class AddTicketTypeHandler : IRequestHandler<AddTicketTypeCommand, Guid>
    {
        private readonly EventDbContext _context;

        public AddTicketTypeHandler(EventDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(AddTicketTypeCommand request, CancellationToken cancellationToken)
        {
            var ticketType = new TicketType
            {
                Id = Guid.NewGuid(),
                EventId = request.EventId,
                Name = request.Name,
                Price = request.Price,
                Quantity = request.Quantity
            };

            _context.TicketTypes.Add(ticketType);
            await _context.SaveChangesAsync(cancellationToken);
            return ticketType.Id;
        }
    }

}

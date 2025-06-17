using System.Net.Sockets;

namespace EventsService
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Venue { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }

    }
}

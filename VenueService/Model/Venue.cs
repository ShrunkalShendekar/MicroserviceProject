using System.ComponentModel.DataAnnotations;

namespace VenueService.Model
{
    public class Venue
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        public int Capacity { get; set; }
    }
}

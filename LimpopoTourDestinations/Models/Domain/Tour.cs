namespace LimpopoTourDestinations.Models.Domain
{
    public class Tour
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int DurationDays { get; set; }
        public Guid? GuideId { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    }
}

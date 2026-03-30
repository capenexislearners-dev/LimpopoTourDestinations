namespace LimpopoTourDestinations.Models.Domain
{
    public class Tour
    {
        public Guid Id { get; set; }
        public string Name { get; set; }  // Required
        public string Description { get; set; }  // Required
        public decimal Price { get; set; }  // Required
        public int DurationDays { get; set; }  // Required
        public string Location { get; set; }  // Required
        public string? TourImageurl { get; set; }
        public Guid? GuideId { get; set; }
        public List<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
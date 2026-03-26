namespace LimpopoTourDestinations.Models.Domain
{
    public class Booking
    { 
        public Guid Id { get; set; }
        public Guid TourId { get; set; }
        public Tour Tour { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public DateTime BookedAt { get;  set; }
        public int NumberOfPeople { get; set; }
    }
}

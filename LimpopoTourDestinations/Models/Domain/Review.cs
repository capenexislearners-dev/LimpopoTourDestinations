namespace LimpopoTourDestinations.Models.Domain
{
    public class Review
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public Guid TourId { get; set; }
    }
}

namespace LimpopoTourDestinations.DTOs
{
    public class CreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public decimal Price { get; set; }
        public string TourImageUrl { get; set; }
        public int GuideId { get; set; }
    }
}

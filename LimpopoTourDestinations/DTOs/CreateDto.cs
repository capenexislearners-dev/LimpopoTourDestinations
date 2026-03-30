namespace LimpopoTourDestinations.Models.DTO
{
    public class CreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int DurationDays { get; set; }
        public string Location { get; set; } = string.Empty;
        public string? TourImageurl { get; set; }
        
    }
}
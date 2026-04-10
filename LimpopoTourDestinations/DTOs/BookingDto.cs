public class BookingDto
{
    public Guid Id { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public DateTime BookedAt { get; set; }
    public int NumberOfPeople { get; set; }

    // Only include tour info you need
    public string? TourName { get; set; }
}
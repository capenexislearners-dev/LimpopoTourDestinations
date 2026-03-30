public class Booking
{
    public Guid Id { get; set; }

    public string? CustomerName { get; set; }

    public string? CustomerEmail { get; set; }

    public DateTime BookedAt { get; set; }

    public int NumberOfPeople { get; set; }
}
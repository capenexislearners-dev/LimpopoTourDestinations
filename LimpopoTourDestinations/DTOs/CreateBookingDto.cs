using System.ComponentModel.DataAnnotations;

namespace LimpopoTourDestinations.Models.DTO
{
    public class CreateBookingDto
    {
        [Required(ErrorMessage = "TourId is required")]
        public Guid TourId { get; set; }
        public string? CustomerName { get; set; }
       
        [Required(ErrorMessage = "CustomerEmail is required")]
        public string? CustomerEmail { get; set; }
        public int NumberOfPeople { get; set; }

        [Required]
        public DateTime BookedAt { get; set; }
    }
}
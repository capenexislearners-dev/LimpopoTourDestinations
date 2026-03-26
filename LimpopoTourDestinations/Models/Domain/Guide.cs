using System.Net.NetworkInformation;

namespace LimpopoTourDestinations.Models.Domain
{
    public class Guide
    { 
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Language { get; set; }
        public string Bio { get; set; }
        public ICollection<Tour> Tour { get; set; } = new List<Tour>();
    }
}

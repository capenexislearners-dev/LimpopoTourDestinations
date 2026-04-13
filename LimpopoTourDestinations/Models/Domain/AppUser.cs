using Microsoft.AspNetCore.Identity;

namespace LimpopoTourDestinations.Models.Domain
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
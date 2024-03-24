using Microsoft.AspNetCore.Identity;

namespace Minimal.API.NET8.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;

namespace MsRazorMovie.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string? FullName { get; set; }
    }
}

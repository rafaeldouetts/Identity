
using Microsoft.AspNetCore.Identity;

namespace Identity.Infra.Repositories.Context
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string ProfilePictureUrl { get; set; }
    }
}

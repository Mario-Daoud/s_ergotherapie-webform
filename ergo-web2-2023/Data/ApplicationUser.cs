using Microsoft.AspNetCore.Identity;

namespace ergo_web2_2023.Data
{
    public class ApplicationUser: IdentityUser
    {
        public byte[]? ProfilePicture { get; set; }
    }
}

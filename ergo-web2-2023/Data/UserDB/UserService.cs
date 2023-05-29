using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using ergo_web2_2023.Helpers;


namespace ergo_web2_2023.Data.UserDB
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService (UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string?> GetUserImageAsync(ClaimsPrincipal user)
        {
            try
            {
                var userId = user.FindFirst(ClaimTypes.NameIdentifier).Value;
                var loggedInUser = await _userManager.FindByIdAsync(userId);
                ImageHelper helper = new ImageHelper();


                var byteArray = loggedInUser.ProfilePicture;
                if (byteArray != null)
                {
                    var type = helper.GetMimeType(byteArray);
                    if (type != null)
                    {
                        var base64 = Convert.ToBase64String(byteArray);
                        var imgSrc = String.Format(type, base64);

                        return imgSrc;
                    }
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            

            return null;

        }
    }
}

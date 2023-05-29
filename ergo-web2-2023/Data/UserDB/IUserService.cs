using System.Security.Claims;

namespace ergo_web2_2023.Data.UserDB
{
    public interface IUserService
    {
        Task<string?> GetUserImageAsync(ClaimsPrincipal user);

    }
}

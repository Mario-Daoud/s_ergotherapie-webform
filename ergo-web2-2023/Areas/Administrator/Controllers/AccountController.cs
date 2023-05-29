using ergo_web2_2023.Areas.Administrator.ViewModels;
using ergo_web2_2023.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace ergo_web2_2023.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles = "Superuser")]
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListAllAdministrators()
        {
            var users = _userManager.Users.Select(u => new ApplicationUserVM
            {
                Id = u.Id,
                UserName = u.UserName
            }).ToList();

            return View(users);
        }
    }
}

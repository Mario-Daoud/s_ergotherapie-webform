using ergo_web2_2023.Areas.Superuser.ViewModels;
using ergo_web2_2023.Data;
using ergo_web2_2023.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ergo_web2_2023.Areas.Superuser.Controllers
{

    [Area("Superuser")]
    [Authorize(Roles = "Superuser")]
    public class AdministratorController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private ErgoDbContext _context;

        public AdministratorController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = new ErgoDbContext();
        }

        public IActionResult EditAdministrators()
        {
            var users = _userManager.Users.ToList().Select(u => new AdministratorVM
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                Phone = u.PhoneNumber,
                Password = u.PasswordHash,
                IsSuperUser = _userManager.IsInRoleAsync(u, "Superuser").Result
            }).ToList();

            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> EditAdministrators(AdministratorVM administratorVM)
        {
            var user = await _userManager.FindByEmailAsync(administratorVM.Email);

            if (user != null)
            {

                if (!string.IsNullOrEmpty(administratorVM.UserName))
                {
                    user.UserName = administratorVM.UserName;
                }

                if (!string.IsNullOrEmpty(administratorVM.Email))
                {
                    user.Email = administratorVM.Email;
                }

                if (!string.IsNullOrEmpty(administratorVM.Phone))
                {
                    user.PhoneNumber = administratorVM.Phone;
                }

                if (!string.IsNullOrEmpty(administratorVM.Password))
                {
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, administratorVM.Password);
                }

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    if (administratorVM.IsSuperUser)
                    {
                        administratorVM.IsSuperUser = true;
                        await _userManager.AddToRoleAsync(user, "Superuser");
                    }
                    else
                    {
                        administratorVM.IsSuperUser = false;
                        IEnumerable<String> roles = new string[] { "Superuser" };
                        await _userManager.RemoveFromRolesAsync(user, roles);
                    }

                    // Check if the user's role has been successfully updated
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var isSuperUser = userRoles.Contains("Superuser");

                    if (isSuperUser == administratorVM.IsSuperUser)
                    {
                        await _context.SaveChangesAsync();

                        var users = _userManager.Users.Select(u => new AdministratorVM
                        {
                            Id = u.Id,
                            UserName = u.UserName,
                            Email = u.Email,
                            Phone = u.PhoneNumber,
                            Password = u.PasswordHash,
                            IsSuperUser = _userManager.IsInRoleAsync(u, "Superuser").Result
                        }).ToList();

                        return View(users);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Error updating user's role");
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(administratorVM);
        }


        [HttpGet("/Superuser/Administrator/GetUserById/{id}")]
        [Route("/Superuser/Administrator/GetUserById/{id}")]
        public IActionResult GetUserById(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            var role = _userManager.IsInRoleAsync(user, "Superuser").Result;
            var IsSuperUser = _userManager.IsInRoleAsync(user, "Superuser").Result;

            return Json(new { id = user.Id, username = user.UserName, email = user.Email, phoneNumber = user.PhoneNumber, isSuperUser = IsSuperUser });
        }

        [HttpGet("/Superuser/Administrator/DeleteUser/{id}")]
        [Route("/Superuser/Administrator/DeleteUser/{id}")]
        public async Task<RedirectToActionResult> DeleteUserAsync(string id)
        {
            await _userManager.DeleteAsync(_userManager.FindByIdAsync(id).Result);
            return RedirectToAction("EditAdministrators");
        }


    }
}

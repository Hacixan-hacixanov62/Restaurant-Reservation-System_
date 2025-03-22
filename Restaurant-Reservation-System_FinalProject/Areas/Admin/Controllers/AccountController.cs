using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_FinalProject.Areas.Admin.ViewModels;

namespace Restaurant_Reservation_System_FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        
        public async Task<IActionResult> CreateAdminUser()
        {
            AppUser appUser = new AppUser
            {
                UserName = "_admin",
                Email = "admin@gmail.com",
                FullName ="UserAdmin"
            };
            IdentityResult identityResult = await _userManager.CreateAsync(appUser, "_Admin123");
            //await _userManager.AddToRoleAsync(appUser, "Admin");
            return Json(identityResult);
            

        }

        public async Task<IActionResult> CreateRole()
        {
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
            await _roleManager.CreateAsync(new IdentityRole("Member"));
            await _roleManager.CreateAsync(new IdentityRole("Superadmin"));
            return Content("added");

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminLoginVM adminLoginVm, string? returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(adminLoginVm);
            }

            var user = await _userManager.FindByNameAsync(adminLoginVm.UsreName);
            //if (user != null)
            //{
            //    ModelState.AddModelError("", "UserName or Password is incorrect");
            //    return View(adminLoginVm);
            //}

            if (user == null || (!await _userManager.IsInRoleAsync(user, "Admin") && !await _userManager.IsInRoleAsync(user, "Superadmin")))
            {
                ModelState.AddModelError("", "UserName or Password is incorrect");
                return View(adminLoginVm);
            }
            var result = await _signInManager.PasswordSignInAsync(user, adminLoginVm.Password, true, true);
            if (!result.Succeeded)
            {

                ModelState.AddModelError("", "UserName or Password is incorrect");
                return View(adminLoginVm);
            }

            if (!string.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Dashboard");
            //return returnUrl != null ? Redirect(returnUrl) : RedirectToAction("Index", "Dashboard");
        }
        public async Task<IActionResult> GetUser()
        {
            var user = await _userManager.GetUserAsync(User);
            return Json(User.Identities);
        }


        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login", "account");
        }
    }
}

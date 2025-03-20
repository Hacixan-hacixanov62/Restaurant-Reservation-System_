using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_FinalProject.ViewModels;

namespace Restaurant_Reservation_System_FinalProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly  SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;
        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,AppDbContext context,RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegisterVM userRegisterVm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser appUser = await _userManager.FindByNameAsync(userRegisterVm.UserName);
            if (appUser != null)
            {
                ModelState.AddModelError("UserName", "This username already exists");
                return View();
            }
            appUser = new()
            {
                UserName = userRegisterVm.UserName,
                FullName = userRegisterVm.FullName,
                Email = userRegisterVm.Email
            };
            var result = await _userManager.CreateAsync(appUser, userRegisterVm.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    //if (error.Code == "DuplicateEmail")
                    //{
                    //    ModelState.AddModelError("Email", "The email already exists");

                    //}
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            await _userManager.AddToRoleAsync(appUser, "Member");

            return RedirectToAction("Login");

        }
    }
}

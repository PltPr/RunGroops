using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Data;
using RunGroopWebApp.Models;
using RunGroopWebApp.ViewModels;

namespace RunGroopWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDBContext _context;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDBContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }
            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                TempData["Error"] = "Wrong credentials";
                return View(loginVM);
            }
            TempData["Error"] = "Wrong credentials!";
            return View(loginVM);

        }
        public IActionResult Registration()
        {
            var response = new RegistrationViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationViewModel registrationVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registrationVM);
            }
            var user = await _userManager.FindByEmailAsync(registrationVM.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email is already in use!";
                return View(registrationVM);
            }
            var newUser = new AppUser()
            {
                Email = registrationVM.EmailAddress,
                UserName = registrationVM.EmailAddress
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registrationVM.Password);

            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Error"] = "Something went wrong!";
                return View(registrationVM);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

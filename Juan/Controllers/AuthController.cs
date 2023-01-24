using Core.Entities;
using Juan.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Juan.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        //Register get
        public IActionResult Register()
        {
            return View();
        }
        //Register Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }
            AppUser appUser = new AppUser()
            {
                UserName = registerVM.Username,
                Fullname = registerVM.Fullname,
                Email = registerVM.Email,
            };
            var identityResult = await _userManager.CreateAsync(appUser, registerVM.Password);
            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(registerVM);
            }
            return Json("ok");
        }
        //Login Get
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid) return View(loginVM);
            var result = await _userManager.FindByEmailAsync(loginVM.UserNameOrEmail);
            if (result == null)
            {
                result = await _userManager.FindByNameAsync(loginVM.UserNameOrEmail);
                if (result == null)
                {
                    ModelState.AddModelError("", "Username or Email is incorrect");
                    return View(loginVM);
                }
            }
            var signInResult = await _signInManager.PasswordSignInAsync(loginVM.UserNameOrEmail, loginVM.Password, loginVM.RememberMe, true);
            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError("", "Sonra gelersen!");
                return View(loginVM);
            }
            if (!signInResult.Succeeded) //Sign in olunma successed deyilse 
            {
                ModelState.AddModelError("", "Username or Email is incorrect");
                return View(loginVM);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}

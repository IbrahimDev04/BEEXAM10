using BEExam10.Enums;
using BEExam10.Models;
using BEExam10.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BEExam10.Controllers
{
    public class AccountController(SignInManager<AppUser> _signInManager,UserManager<AppUser> _userManager, RoleManager<IdentityRole> _roleManager) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {

            if(!ModelState.IsValid) return View(vm);

            AppUser user = new AppUser
            {
                Name = vm.Name,
                Email = vm.Email,
                UserName = vm.Username,
                Surname = vm.Surname,
            };

            var result = await _userManager.CreateAsync(user, vm.Password);

            if(!result.Succeeded)
            {
                ModelState.AddModelError("", "Something is wrong");
                
            }
            if (!ModelState.IsValid) return View(vm);

            await _userManager.AddToRoleAsync(user, UserRoles.Member.ToString());

            return RedirectToAction("Login", "Account");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            AppUser user = await _userManager.FindByNameAsync(vm.UsernameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(vm.UsernameOrEmail);
                if(user == null)
                {
                    ModelState.AddModelError("", "Something is wrong");
                }
            }

            if (!ModelState.IsValid) return View(vm);

            await _signInManager.CheckPasswordSignInAsync(user, vm.Password, true);
            var result = await _signInManager.PasswordSignInAsync(user, vm.Password, vm.RememberMe, true);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Something is wrong");
                
            }
            if (!ModelState.IsValid) return View(vm);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Signout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CreateRole()
        {
            foreach (var role in Enum.GetValues(typeof(UserRoles)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole
                    {
                        Name = role.ToString()
                    });
                }                
            }

            return Content("Ok");
        }
    }
}

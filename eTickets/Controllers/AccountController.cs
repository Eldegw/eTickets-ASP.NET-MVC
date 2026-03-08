using eTickets.Data;
using eTickets.Data.Static;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager ;
        private readonly AppDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }


        public async Task<IActionResult> Users()
        {
            var user = await _context.Users.ToListAsync();
            return View("Users" , user);
        }


        public IActionResult Login(LoginVM loginVM)
        {
          return View("Login" , loginVM);
        }

        [HttpPost]
        public async Task<IActionResult> SaveLogin(LoginVM loginVM)
        {
        
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);

                if (user != null)
                {
                 
                    var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);

                    if (passwordCheck)
                    {
                       
                        var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);

                        if (result.Succeeded)
                        {
                         
                            return RedirectToAction("Index", "Movies");
                        }
                    }

                   
                    TempData["Error"] = "Email Or Password Is Wrong. Please Try Again";
                    return View("Login", loginVM);
                }

             
                TempData["Error"] = "User not found. Please, try again!";
                return View("Login", loginVM);

            }
            return View("Login",loginVM);



        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Movies");
        }

        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public async Task<IActionResult> SaveRegister(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View("Register", registerVM);

            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View("Register", registerVM);
            }

            var newUser = new ApplicationUser()
            {
                FullName = registerVM.FullName,
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserResponse.Succeeded)
            {
              
                await _userManager.AddToRoleAsync(newUser, "User");

              
                await _signInManager.PasswordSignInAsync(newUser, registerVM.Password, false, false);

                TempData["Success"] = "Account created successfully! Welcome to eTickets.";

                return RedirectToAction("RegisterCompleted");
            }

            foreach (var error in newUserResponse.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View("Register", registerVM);
        }
        public IActionResult RegisterCompleted()
        {
            return View();
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View("AccessDenied" , ReturnUrl);
        }
    }
}

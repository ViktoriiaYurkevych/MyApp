using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyApp.Data.Models;
using MyApp.Services;
using MyApp.ViewModels;

namespace UsersApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AplicationUser> signInManager;
        private readonly UserManager<AplicationUser> userManager;
        private readonly AuthenticationService authenticationService;

        public AccountController(SignInManager<AplicationUser> signInManager, UserManager<AplicationUser> userManager, AuthenticationService authenticationService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.authenticationService = authenticationService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
                {
                    await signInManager.SignInWithClaimsAsync(user, false, await authenticationService.GenerateClaims(user));
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Email or password is incorrect.");
                    return View(model);
                }
            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                AplicationUser aplicationuser = new AplicationUser
                {
                    FullName = model.UserName,
                    Email = model.Email,
                    UserName = model.Email,
                };

                var result = await userManager.CreateAsync(aplicationuser, model.Password);

                if (result.Succeeded)
                {
                    
                    var roleResult = await userManager.AddToRoleAsync(aplicationuser, model.Role);

                    if (roleResult.Succeeded)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        foreach (var error in roleResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }


        public IActionResult VerifyEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyEmail(EmailVM model)
        {
            if (ModelState.IsValid)
            {
                var aplicationuser = await userManager.FindByNameAsync(model.Email);

                if (aplicationuser == null)
                {
                    ModelState.AddModelError("", "Something is wrong!");
                    return View(model);
                }
                else
                {
                    return RedirectToAction("ChangePassword", "Account", new { username = aplicationuser.UserName });
                }
            }
            return View(model);
        }

        public IActionResult ChangePassword(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("VerifyEmail", "Account");
            }
            return View(new ChangePasswordVM { Email = username });
        }


        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var aplicationuser = await userManager.FindByNameAsync(model.Email);
                if (aplicationuser != null)
                {
                    var result = await userManager.RemovePasswordAsync(aplicationuser);
                    if (result.Succeeded)
                    {
                        result = await userManager.AddPasswordAsync(aplicationuser, model.NewPassword);

                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {

                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email not found!");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong. try again.");
                return View(model);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
           
            
        }
    }
}
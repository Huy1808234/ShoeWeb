using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Project01.Models;
using System.Text.Encodings.Web;
using System.Text;
using Project01.AppData;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Project01.Controllers
{

    public class UserController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserStore<AppUser> _userStore;
        //private readonly IUserEmailStore<IdentityUser> _emailStore;
        public UserController(
            UserManager<AppUser> userManager,
            IUserStore<AppUser> userStore,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _userStore = userStore;
            _signInManager = signInManager;

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    //_logger.LogInformation("User logged in.");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("login", "Emaail va Mat khau khong trung nhau.");
                    return View();
                }
            }
                return View();
        }
        [HttpPost]

        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {

            returnUrl ??= Url.Content("~/");
           
            if (ModelState.IsValid)
            {
                var user = CreateUser();
                // Lưu thông tin FirstName và LastName
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Address = model.Address;

                await _userStore.SetUserNameAsync(user, model.Email, CancellationToken.None);
              
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "Home");
                       

                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View();
        }
        private AppUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<AppUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(AppUser)}'. " +
                    $"Ensure that '{nameof(AppUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            //_logger.LogInformation("User logged out.");
            return RedirectToAction("Index","Home");
        }

    }
        }

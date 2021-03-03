using WebApplication1.PresentationLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication1.DomainLayer.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Web.Areas.Admin.Controllers
{


    
    public class AccountController : Controller
    {
        #region Constructor
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }
        #endregion

        #region Login

        [AllowAnonymous]
        public IActionResult Login() => View();

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginVM vm, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(vm.Email);
                if (user != null)  
                {
                    await _signInManager.SignOutAsync();  
                    var result = await _signInManager.PasswordSignInAsync(user, vm.Password, false, false);
                    if (result.Succeeded) 
                    {
                        return Redirect(ReturnUrl ?? @"\home\"); 
                    }
                }

                
                ModelState.AddModelError("Email", "Invalid mail or password");
            }
            //ViewBag.ReturnUrl = url;
            ViewBag.ReturnUrl = ReturnUrl;
            return View(vm);
        }
        #endregion

        #region Logout
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login", "account");
        }
        #endregion
    }
}

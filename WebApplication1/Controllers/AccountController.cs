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
                // kullanıcıyı mail adresine göre buluyoruz
                var user = await _userManager.FindByEmailAsync(vm.Email);
                if (user != null)  // eğer, mail adresine uyan bir kullanıcı var ise, login işlemlerini yapıyoruz.
                {
                    await _signInManager.SignOutAsync();  // öncelik cookie vs. gibi yapılarda kullanıcının bilgileri kaldıysa, çıkış işlemini yaptırıp sonrasında tekrar login olmasını sağlıyoruz.
                    var result = await _signInManager.PasswordSignInAsync(user, vm.Password, false, false);
                    if (result.Succeeded) // kullanıcı başarılı bir şekilde giriş yaptıysa, yönlendirme işlemlerini yapıyoruz.
                    {
                        return Redirect(ReturnUrl ?? @"\home\"); // eğer kullanıcı direkt olarak login sayfasını açtıysa, giriş yaptıktan sonra admin\home\index ' yönlendiriyoruz. eğer kullanıcı url üzerinden login olmadan bir adrese girmeye çalışırsa önce login olmasını sağlıyoruz ve gitmek istediği adrese yönlendiriyoruz
                    }
                }

                // kullanıcı bilgilerine uyan bir kullanıcı yok ise,
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

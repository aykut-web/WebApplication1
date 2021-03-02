using WebApplication1.PresentationLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WebApplication1.DomainLayer.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Web.Areas.Admin.Controllers
{


    [Authorize]

    public class AdminController : Controller
    {
        #region Constructor
        private readonly UserManager<User> _userManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private IPasswordValidator<User> _passwordValidator;

        public AdminController(UserManager<User> userManager, IPasswordHasher<User> passwordHasher, IPasswordValidator<User> passwordValidator)
        {
            this._userManager = userManager;
            this._passwordHasher = passwordHasher;
            this._passwordValidator = passwordValidator;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.Users.ToListAsync();

            //var roles = await _userManager.GetRolesAsync();

            return View(user);
        }
        #endregion

        #region Create
        
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Create(UserVm vm)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.UserName = vm.UserName;
                user.Email = vm.Email;

                var result = await _userManager.CreateAsync(user, vm.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(actionName: nameof(Index));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(vm);
        }
        #endregion

        #region Edit 
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id.HasValue)
            {
                var user = await _userManager.FindByIdAsync(id.Value.ToString());
                if (user != null)
                {
                    UserVm vm = new UserVm
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        Password = user.PasswordHash
                    };
                    return View(vm);
                };
                return RedirectToAction(actionName: nameof(Index));
            }
            return View(viewName: nameof(Index), model: _userManager.Users);
        }


        [
            HttpPost,
            ValidateAntiForgeryToken
        ]
        public async Task<IActionResult> Edit(UserVm vm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(vm.Id.ToString());
                if (user != null)
                {
                    user.Email = vm.Email;
                    user.UserName = vm.Email;
                    IdentityResult validPassword = null;

                    // Password güncelleme işlemi
                    if (!string.IsNullOrWhiteSpace(vm.Password))
                    {
                        validPassword = await _passwordValidator.ValidateAsync(_userManager, user, vm.Password);
                        if (validPassword.Succeeded)
                        {
                            user.PasswordHash = _passwordHasher.HashPassword(user, vm.Password);
                        }
                        else
                        {
                            foreach (var item in validPassword.Errors)
                            {
                                ModelState.AddModelError("", item.Description);
                            }
                        }
                    }

                    // Kullanıcıyı db üzeriden güncelleme işlemi
                    if (validPassword.Succeeded)
                    {
                        var result = await _userManager.UpdateAsync(user);
                        if (result.Succeeded)
                        {
                            return RedirectToAction(actionName: nameof(Index));
                        }
                        else
                        {
                            foreach (var item in validPassword.Errors)
                            {
                                ModelState.AddModelError("", item.Description);
                            }
                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "user not found");
            }
            return View(viewName: nameof(Index), model: _userManager.Users);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id.HasValue)
            {
                var user = await _userManager.FindByIdAsync(id.Value.ToString());
                if (user != null)
                {
                    var result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(actionName: nameof(Index));
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "user not found");
            }
            return View(viewName: nameof(Index), _userManager.Users);
        }
        #endregion
    }
}
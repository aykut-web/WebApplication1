using WebApplication1.PresentationLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApplication1.DomainLayer.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Web.Areas.Admin.Controllers
{

    [Authorize]
    public class RolesController : Controller
    {
        #region Constructor
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
        }
        #endregion

        #region Index
        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }


        [
            HttpPost,
            ValidateAntiForgeryToken
        ]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(name.ToLower()));
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            ModelState.AddModelError("", "role not found");
            return View(model: name);
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            IdentityRole _role = await _roleManager.FindByIdAsync(id);
            var members = new List<User>();
            var nonMembers = new List<User>();

            foreach (var user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, _role.Name) ? members : nonMembers;
                list.Add(user);
            }

            var model = new RoleDetailsVM
            {
                Role = _role,
                Members = members,
                NonMembers = nonMembers
            };
            return View(model: model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleEditVM vm)
        {
            IdentityResult _result;

            if (ModelState.IsValid)
            {
                // Kullanıcıları Role ekleme işlemi
                foreach (var userId in vm.IdsToAdd ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        _result = await _userManager.AddToRoleAsync(user, vm.RoleName);
                        if (!_result.Succeeded)
                        {
                            foreach (var item in _result.Errors)
                            {
                                ModelState.AddModelError("", item.Description);
                            }
                        }
                    }
                }

                // Kullanıcıları Rolden silme
                foreach (var userId in vm.IdsToDelete ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        _result = await _userManager.RemoveFromRoleAsync(user, vm.RoleName);
                        if (!_result.Succeeded)
                        {
                            foreach (var item in _result.Errors)
                            {
                                ModelState.AddModelError("", item.Description);
                            }
                        }
                    }
                }
            }

            if (ModelState.IsValid)
            {
                return RedirectToAction(actionName: nameof(Index));
            }
            else
            {
                return RedirectToAction(actionName: nameof(Edit), routeValues: new { id = vm.Id });
            }
        }
        #endregion
    }
}

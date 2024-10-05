using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_PL_.Models;

namespace Project_PL_.Controllers
{
    public class RoleController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;
        public RoleController(RoleManager<IdentityRole> roleManager , UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string InputSearch)
        {
            var roles = Enumerable.Empty<RoleViewModel>();
            if (string.IsNullOrEmpty(InputSearch))
            {
                roles = await _roleManager.Roles.Select(R => new RoleViewModel()
                {
                    Id = R.Id,
                    Name = R.Name
                }
               ).ToListAsync();
            }
            else
            {
                roles = await _roleManager.Roles.Where(R => R.Name.ToLower().Contains(InputSearch.ToLower()))
                .Select(R => new RoleViewModel()
                {
                    Id = R.Id,
                    Name = R.Name
                }).ToListAsync();

            }
            return View(roles);
        }
        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {
            if (id is null) return BadRequest();
            var rolefromDb = await _roleManager.FindByIdAsync(id);
            if (rolefromDb is null) return NotFound();

            var user = new RoleViewModel()
            {
                Id = rolefromDb.Id,
                Name = rolefromDb.Name
            };
            return View(viewName, user);
        }
        public async Task<IActionResult> Edit([FromRoute] string? id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] string? id, RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (id is null) return BadRequest();
                var rolefromdb = await _roleManager.FindByIdAsync(id);
                if (rolefromdb is null) return NotFound();
                rolefromdb.Name = model.Name;

                var count = await _roleManager.UpdateAsync(rolefromdb);
                if (count.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Edit");
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var Role = new IdentityRole()
                {
                    Name = model.Name
                };
                var count = await _roleManager.CreateAsync(Role);
                if (count.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Create");
            return View(model);
        }
        public async Task<IActionResult> Delete([FromRoute] string? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] string? id, RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (id is null) return BadRequest();
                var rolefromdb = await _roleManager.FindByIdAsync(id);
                if (rolefromdb is null) return NotFound();
                rolefromdb.Name = model.Name;


                var count = await _roleManager.DeleteAsync(rolefromdb);
                if (count.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Delete");
            return View(model);
        }

        public async Task<IActionResult> AddOrRemoveUser(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (roleId is null) return NotFound();
          
            var UsersInRole = new List<UsersInRoleViewModel>();

            var users = await _userManager.Users.ToListAsync();
            ViewData["RoleId"] = roleId;

            foreach (var user in users)
            {
                var userinrole = new UsersInRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if(await _userManager.IsInRoleAsync(user,role.Name))
                {
                    userinrole.IsSelected = true;
                }
                else
                {
                    userinrole.IsSelected = false;
                }

                UsersInRole.Add(userinrole);
            }

            return View(UsersInRole);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUser(string roleId , List<UsersInRoleViewModel> users)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if(role is null) return BadRequest();
            if(ModelState.IsValid)
            {
                foreach(var user in users)
                {
                    var appuser = await _userManager.FindByIdAsync(user.UserId);
                    if(appuser is not null)
                    {
                        if (user.IsSelected && !await _userManager.IsInRoleAsync(appuser, role.Name))
                        {
                            await _userManager.AddToRoleAsync(appuser, role.Name);
                        }
                        else if (!user.IsSelected && await _userManager.IsInRoleAsync(appuser, role.Name))
                        {
                            await _userManager.RemoveFromRoleAsync(appuser, role.Name);
                        }
                    }

                }

                return RedirectToAction(nameof(Edit), new { Id = roleId });
            }
            return View(users);
        }

    }
}

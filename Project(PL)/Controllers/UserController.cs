using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_PL_.Models;

namespace Project_PL_.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		public UserController(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}
		public async Task<IActionResult> Index(string InputSearch)
		{
			var user = Enumerable.Empty<UserViewModel>();
			if(string.IsNullOrEmpty(InputSearch))
			{
				user = await _userManager.Users.Select(U => new UserViewModel()
				{
					Id = U.Id,
					FName = U.FName,
					LName = U.LName,
					Email = U.Email,
					Roles = _userManager.GetRolesAsync(U).Result
				}
			   ).ToListAsync();
			}
			else
			{
				user = await _userManager.Users.Where(U => U.Email.ToLower().Contains(InputSearch.ToLower()))
				.Select(U => new UserViewModel()
				{
                    Id = U.Id,
                    FName = U.FName,
					LName = U.LName,
					Email = U.Email,
					Roles = _userManager.GetRolesAsync(U).Result
				}).ToListAsync();
				
			}
			return View(user);
		}
        public async Task<IActionResult> Details(string? id , string viewName = "Details")
		{
			if (id is null) return BadRequest();
			var userfromDb = await _userManager.FindByIdAsync(id);
				if (userfromDb is null) return NotFound();

			var user = new UserViewModel()
			{
				Id = userfromDb.Id,
				FName = userfromDb.FName,
				LName = userfromDb.LName,
				Email = userfromDb.Email,
				Roles = _userManager.GetRolesAsync(userfromDb).Result
			};
				return View(viewName, user);
		}
        public async Task<IActionResult> Edit([FromRoute]string? id)
		{
			return await Details(id, "Edit");
		}
		[HttpPost]
        public async Task<IActionResult> Edit([FromRoute] string? id , UserViewModel model)
        {
            if(ModelState.IsValid)
			{
				if (id is null) return BadRequest();
				var userfromdb = await _userManager.FindByIdAsync(id);
				if (userfromdb is null) return NotFound();
				userfromdb.Email = model.Email;
				userfromdb.FName = model.FName;
                userfromdb.LName = model.LName;

				var count = await _userManager.UpdateAsync(userfromdb);
				if(count.Succeeded)
				{
					return RedirectToAction("Index");
				}
			}
			ModelState.AddModelError(string.Empty, "Invalid Edit");
			return View(model);
        }

        public async Task<IActionResult> Delete([FromRoute] string? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] string? id, UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (id is null) return BadRequest();
                var userfromdb = await _userManager.FindByIdAsync(id);
                if (userfromdb is null) return NotFound();
                userfromdb.Email = model.Email;
                userfromdb.FName = model.FName;
                userfromdb.LName = model.LName;

                var count = await _userManager.DeleteAsync(userfromdb);
                if (count.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Edit");
            return View(model);
        }
    }
}

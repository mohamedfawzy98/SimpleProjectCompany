using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_PL_.Helper;
using Project_PL_.Models;
using System.Data;

namespace Project_PL_.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
		public IActionResult SignUp()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByNameAsync(model.UserName);
				if (user is null)
				{
					user = await _userManager.FindByEmailAsync(model.Email);
					if (user is null)
					{

						user = new ApplicationUser()
						{
							UserName = model.UserName,
							FName = model.FName,
							LName = model.LName,
							Email = model.Email

						};
						var result = await _userManager.CreateAsync(user, model.Password);
						if (result.Succeeded)
						{
							return RedirectToAction("SignIn");
						}

					}
					ModelState.AddModelError(string.Empty, "Email Is Already Exsit");
				}
			}
			ModelState.AddModelError(string.Empty, "UserName Is Already Exsit");
			return View(model);
		}
		public IActionResult SignIn()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> SignIn(SignInViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					// check Email
					var user = await _userManager.FindByEmailAsync(model.Email);
					if (user is not null)
					{
						/// check password
						bool Flag = await _userManager.CheckPasswordAsync(user, model.Password);

						if (Flag)
						{
							// sign in
							//var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RemmemberMe, false);
							var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RemmemberMe, false);
							if (result.Succeeded)
							{

								return RedirectToAction("Index", "Home");
							}


						}
					}
				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);
				}


			}
			ModelState.AddModelError(string.Empty, "Invalid Error");

			return View(model);
		}
		public async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("SignIn");
		}
		public async Task<IActionResult> ForgetPassword()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> SendRestPasswordUrl(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user is not null)
				{
					// create token
					var token = await _userManager.GeneratePasswordResetTokenAsync(user);
					// create URL
					var url = Url.Action("RestPassword", "Account", new { email = model.Email , token }, Request.Scheme);

					// create Email
					var email = new Email()
					{
						To = model.Email,
						Subject = "RestPassword",
						Body = url
					};

					// Send Email
					EmailSetting.SendEmail(email);
					return RedirectToAction("CheckYourInBox");

				}
			}
			ModelState.AddModelError(string.Empty, "Invaild");
			return View();
		}

		public IActionResult CheckYourInBox()
		{
			return View();
		}
		public IActionResult RestPassword(string email, string token)
		{
			TempData["email"] = email;
			TempData["token"] = token;

			return View();
		}
		[HttpPost]
		public async Task<IActionResult> RestPassword(RestPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var email = TempData["email"] as string;
				var token = TempData["token"] as string;

				var user = await _userManager.FindByEmailAsync(email);
				if (user is not null)
				{
					var result = await _userManager.ResetPasswordAsync(user, token, model.Password);

					if (result.Succeeded)
					{
						return RedirectToAction("SignIn");
					}
				}
			}

			ModelState.AddModelError(string.Empty, "Rest Password Invalid");
			return View(model);
		}
	}
}

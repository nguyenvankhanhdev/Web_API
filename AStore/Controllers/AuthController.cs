using AStore_Library;
using AStore_Web.Models;
using AStore_Web.Models.DTO;
using AStore_Web.Service.IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace AStore_Web.Controllers
{
	public class AuthController : Controller
	{
		private readonly IAuthService _authService;
		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpGet]
		public IActionResult Login()
		{
			LoginRequestDTO model = new LoginRequestDTO();
			ViewBag.Layout= "_FrontendLayout";
			return View(model);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginRequestDTO model)
		{
			APIResponse aPIResponse = await _authService.LoginAsync<APIResponse>(model);
			if(aPIResponse.IsSuccess && aPIResponse != null)
			{
				LoginResponseDTO loginResponseDTO = JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(aPIResponse.Result));
				var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
				identity.AddClaim(new Claim(ClaimTypes.Name, loginResponseDTO.user.Username));
				identity.AddClaim(new Claim(ClaimTypes.Role, loginResponseDTO.user.Role));
				var principal = new ClaimsPrincipal(identity);
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
				HttpContext.Session.SetString(SD.SessionToken, loginResponseDTO.Token);
				return RedirectToAction("Index", "Product");
			}
			else
			{
				ModelState.AddModelError("CustomError", aPIResponse.ErrorMessages.FirstOrDefault());
				return View(model);
			}
		}
		[HttpGet]
		public IActionResult Register()
		{
			ViewBag.Layout = "_FrontendLayout";
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterRequestDTO registerRequest)
		{
			APIResponse aPIResponse = await _authService.RegisterAsync<APIResponse>(registerRequest);
			if (aPIResponse.IsSuccess && aPIResponse != null)
			{
				return RedirectToAction("Login");
			}
			else
			{
				ModelState.AddModelError("CustomError", aPIResponse.ErrorMessages.FirstOrDefault());
				return View();
			}
		}
		public async Task<IActionResult> LogOut()
		{
			await HttpContext.SignOutAsync();
			HttpContext.Session.SetString(SD.SessionToken, "");
			return RedirectToAction("Login");
		}
		public IActionResult AccessDenied()
		{
			return View();
		}

	}
}

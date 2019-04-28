using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Data.Models;
using Service.Interfaces;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private IUserService _service { get; }
        public HomeController(IUserService serv)
        {
            _service = serv;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Signs() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var sha256 = new SHA256Managed();
                User user = _service.GetUserByName(model.Email, Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(model.Password))));
                if (user != null)
                {
                    await Authenticate(user); // аутентификация
                }
                else ModelState.AddModelError("","Неверно введен логин и/или пароль.");
            }
            return PartialView("_Login", model);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var sha256 = new SHA256Managed();
                User user = _service.GetUserByName(model.Email, Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(model.Password))));
                if (user == null)
                {
                    // добавляем пользователя в бд
                    _service.AddUser(new User() { Name = model.Name, Email = model.Email, PasswordHash = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(model.Password))) });
                    await Authenticate(user); // аутентификация
                }
                else ModelState.AddModelError("", "Уже существует такой пользователь.");
            }
            return PartialView("_Register",model);
        }

        private async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email)
            };
            if(user.IsAdmin)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, "admin"));
            }
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
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
using Microsoft.EntityFrameworkCore.Internal;
using Service.Interfaces;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITestService _testService;
        private readonly ISignService _signService;
        public HomeController(IUserService serv, ITestService srv, ISignService sign)
        {
            _userService = serv;
            _testService = srv;
            _signService = sign;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserPage()
        {
            var user = _userService.GetUser(User.FindFirstValue(ClaimsIdentity.DefaultIssuer));
            var model = new UserPageViewModel();
            model.AnsPercent = _testService.GetCorrectAnsPercent(user);
            model.UserName = user.Name;
            model.UserTests = _testService.GetUserTests(user);
            return View(model);
        }

        public IActionResult Signs(string type) => View(new SignsViewModel()
        {
            Signs = _signService.GetTypesSigns(type),
            Types = _signService.GetTypesSigns(type).Select(x=>x.Type).Distinct((x,y)=>x.ID == y.ID).ToList()
        });

        [HttpGet]
        public IActionResult Tests(string type)
        {
            var user = _userService.GetUser(User.FindFirstValue(ClaimsIdentity.DefaultIssuer));
            var model = _testService.AddUserTest(user, type);
            return View(model);
        }

        //[HttpPost]
        //public IActionResult Tests(Test model)
        //{
        //    _testService.FinishUserTest(model);
        //    return RedirectToAction("UserPage","Home");
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password.Length < 5)
                {
                    ModelState.AddModelError("Password", "Пароль должен содержать не менее 5 символов");
                    return PartialView("_Login", model);
                }
                var sha256 = new SHA256Managed();
                User user = _userService.GetUserByName(model.Email, Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(model.Password))));
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
                if (model.Password.Length < 5)
                {
                    ModelState.AddModelError("Password","Пароль должен содержать не менее 5 символов");
                    return PartialView("_Register", model);
                }
                var sha256 = new SHA256Managed();
                User user = _userService.GetUserByName(model.Email, Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(model.Password))));
                if (user == null)
                {
                    // добавляем пользователя в бд
                    user = _userService.AddUser(new User() { Name = model.Name, Email = model.Email, PasswordHash = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(model.Password))) });
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
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultIssuer, user.ID)
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

        [HttpPost]
        public IActionResult SaveExam([FromBody]Test test)
        {
            _testService.FinishUserTest(test);
            return RedirectToAction("UserPage", "Home");
        }
    }
}
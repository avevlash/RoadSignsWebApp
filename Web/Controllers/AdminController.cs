using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using Web.ViewModels;

namespace Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly ISignService _signService;
        private readonly IQuestionService _questService;
        private readonly IUserService _userService;
        public AdminController(ISignService service, IQuestionService srv, IUserService usrv)
        {
            _signService = service;
            _questService = srv;
            _userService = usrv;
        }
        [HttpGet]
        public IActionResult AddSign() => View(new AddSignViewModel()
        {
            Types = _signService.GetSignTypes()
        });
        [HttpGet]
        public IActionResult EditSign(int modelid)=> View(new AddSignViewModel()
        {
            Sign = _signService.GetSign(modelid),
            Types = _signService.GetSignTypes()

        });
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditSign(AddSignViewModel model)
        {
            model.Types = _signService.GetSignTypes();
            if (!ModelState.IsValid)
                return View(model);
            else
            {
                if (model.Image != null)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(model.Image.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)model.Image.Length);
                    }
                    model.Sign.Image = imageData;
                }
                _signService.AddSign(model.Sign);
                return RedirectToAction("ManageSigns", "Admin");
            }
        }

        [HttpDelete]
        public void RemoveSign(int signid) => _signService.RemoveSign(_signService.GetSign(signid));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSign(AddSignViewModel model)
        {
            model.Types = _signService.GetSignTypes();
            if (!ModelState.IsValid)
                return View(model);
            else
            {
                if (model.Image == null && model.Sign.Image == null)
                {
                    ModelState.AddModelError("Sign.Image", "Выберите файл изображения");
                    return View(model);
                }
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(model.Image.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)model.Image.Length);
                }
                model.Sign.Image = imageData;
                _signService.AddSign(model.Sign);
                return RedirectToAction("AddSign");
            }
        }
        public IActionResult ManageSigns() => View(_signService.GetTypesSigns(""));

        public IActionResult ManageQuestions() => View(_questService.GetAllQuestions());

        public IActionResult AddQuestion() =>  View(new AddQuestionViewModel(){Question = new Question(), Signs = _signService.GetTypesSigns("")});
        public IActionResult EditQuestion(int modelid) => View(new AddQuestionViewModel()
        {
            Question = _questService.GetQuestion(modelid),
            Signs = _signService.GetTypesSigns("")
        });

        [HttpDelete]
        public void RemoveQuestion(int modelid) => _questService.RemoveQuestion(_questService.GetQuestion(modelid));
        [HttpPost]
        public IActionResult AddQuestion(AddQuestionViewModel model)
        {
                model.Signs = _signService.GetTypesSigns("");
                model.Question.Sign = _signService.GetSign(model.Question.Sign.ID);
                if (model.Question.Variants.Count < 2 || model.Question.Variants.Count > 5)
                {
                    ModelState.AddModelError("Question.Variants","Количество вариантов должно быть от 2 до 5");
                    return View(model);
                }
                if (string.IsNullOrEmpty(model.Question.Text))
                {
                    ModelState.AddModelError("Question.Text", "Введите текст вопроса");
                    return View(model);
                }
                if (!(model.Question.ForKids||model.Question.ForBikers||model.Question.ForDrivers||model.Question.ForPedestrians))
                {
                    ModelState.AddModelError("Question.ForKids", "Выберите хотя бы один из типов.");
                    return View(model);
                }
            
                _questService.AddQuestion(model.Question);
                return RedirectToAction("AddQuestion");
        }

        [HttpPost]
        public IActionResult EditQuestion(AddQuestionViewModel model)
        {
            model.Signs = _signService.GetTypesSigns("");
            model.Question.Sign = _signService.GetSign(model.Question.Sign.ID);
            if (model.Question.Variants.Count < 2 || model.Question.Variants.Count > 5)
            {
                ModelState.AddModelError("Question.Variants", "Количество вариантов должно быть от 2 до 5");
                return View(model);
            }
            if (string.IsNullOrEmpty(model.Question.Text))
            {
                ModelState.AddModelError("Question.Text", "Введите текст вопроса");
                return View(model);
            }
            if (!(model.Question.ForKids || model.Question.ForBikers || model.Question.ForDrivers || model.Question.ForPedestrians))
            {
                ModelState.AddModelError("Question.ForKids", "Выберите хотя бы один из типов.");
                return View(model);
            }
            _questService.EditQuestion(model.Question);
            return RedirectToAction("ManageQuestions", "Admin");
        }

        public IActionResult ManageUsers() => View(_userService.GetUsers());

        public IActionResult AddAdmin(string user)
        {
            _userService.ToggleAdmin(user);
            return RedirectToAction("ManageUsers", "Admin");
        }

        public IActionResult RemoveUser(string id)
        {
            User user = _userService.GetUser(id);
            _userService.RemoveUser(user);
            return RedirectToAction("ManageUsers", "Admin");
        }
    }
}
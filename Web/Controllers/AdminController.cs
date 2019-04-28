using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Web.ViewModels;

namespace Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly ISignService _signService;
        private readonly IQuestionService _questService;
        public AdminController(ISignService service, IQuestionService srv)
        {
            _signService = service;
            _questService = srv;
        }
        [HttpGet(Name = "Creating")]
        public IActionResult AddSign(Sign model) => model != null ? View(model): View();
        [HttpGet(Name = "Editing")]
        public IActionResult EditSign(int modelid)=> RedirectToAction("AddSign", "Admin", _signService.GetSign(modelid));
        [HttpDelete]
        public void RemoveSign(int signid) => _signService.RemoveSign(_signService.GetSign(signid));

        [HttpPost]
        public IActionResult AddSign(Sign model, IFormFile uploadImage)
        {
            if (!ModelState.IsValid)
                return View(model);
            else
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(uploadImage.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)uploadImage.Length);
                }
                model.Image = imageData;
                bool test = model.ID != 0;
                _signService.AddSign(model);
                if (test) return RedirectToAction("ManageSigns", "Admin");
                return View(new Sign());
            }
        }
        public IActionResult ManageSigns() => View(_signService.GetTypesSigns(""));

        public IActionResult ManageQuestions() => View(_questService.GetAllQuestions());

        [HttpGet(Name = "Create")]
        public IActionResult AddQuestion(Question model) =>  View(new AddQuestionViewModel(){Question = model, Signs = _signService.GetTypesSigns("")});
        [HttpGet(Name = "Edit")]
        public IActionResult EditQuestion(int id) => RedirectToAction("AddQuestion","Admin", _questService.GetQuestion(id));
        [HttpDelete]
        public void RemoveQuestion(int id) => _questService.RemoveQuestion(_questService.GetQuestion(id));
        [HttpPost]
        public IActionResult AddQuestion(AddQuestionViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            else
            {
                if (model.Question.Variants.Count < 2 || model.Question.Variants.Count > 5 ||
                    model.Question.Sign == null)
                {
                    ModelState.AddModelError("","Проверьте количество вариантов и выбор знака.");
                    return View(model);
                }
                else
                {
                    if (model.Question.ID != 0)
                    {
                        _questService.EditQuestion(model.Question);
                        return RedirectToAction("ManageQuestions", "Admin");
                    }
                    else
                    {
                        _questService.AddQuestion(model.Question);
                        return View(new AddQuestionViewModel() {Signs = _signService.GetTypesSigns("")});
                    }
                }
            }
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentWebApplication.Helpers;
using StudentWebApplication.Models;
using StudentWebApplication.Services;

namespace StudentWebApplication.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentService _service;

        public StudentController(StudentService service)
        {
            _service = service;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin,Viewer")]
        public async Task<IActionResult> Index()
        {
            var students = await _service.GetAllAsync();
            var token = HttpContext.Session.GetString("JWToken");

            if (JwtHelper.IsAdmin(token))
            {
                return View("IndexAdmin", students);
            }
            else
            {
                return View("IndexViewer", students);
            }
            //return View(students);
        }



        [HttpGet]
        public async Task<IActionResult> Details(int id )
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (!JwtHelper.IsAdmin(token))
                return RedirectToAction("Index");

            var student = await _service.GetByIdAsync(id);
            if (student == null)
            {
                return View("NotFound");
            }
            return View(student);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (!JwtHelper.IsAdmin(token))
                return RedirectToAction("Index");

            return View();
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Student student)
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (!JwtHelper.IsAdmin(token))
                return RedirectToAction("Index");

            await _service.AddAsync(student);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (!JwtHelper.IsAdmin(token))
                return RedirectToAction("Index");

            var student = await _service.GetByIdAsync(id);
            return View(student);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Student student)
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (!JwtHelper.IsAdmin(token))
                return RedirectToAction("Index");


            await _service.UpdateAsync(student);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (!JwtHelper.IsAdmin(token))
                return RedirectToAction("Index");

            var student = await _service.GetByIdAsync(id);
            return View(student);
        }

        
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (!JwtHelper.IsAdmin(token))
                return RedirectToAction("Index");


            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}






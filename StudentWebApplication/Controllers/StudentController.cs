using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using StudentWebApplication.Models;

namespace StudentWebApplication.Controllers
{
    public class StudentController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;

        public StudentController(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Account");

            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(_config["ApiSettings:BaseUrl"] + "/api/student");

            if (!response.IsSuccessStatusCode)
                return Unauthorized();

            var result = await response.Content.ReadAsStringAsync();
            var students = JsonConvert.DeserializeObject<List<Student>>(result);

            return View(students);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudentWebApplication.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace StudentWebApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;

        public AccountController(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegister model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var client = _clientFactory.CreateClient();
            var apiUrl = _config["ApiSettings:BaseUrl"] + "/api/auth/register";

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Registration failed");
                return View(model);
            }

            // Redirect to login page after successful signup
            return RedirectToAction("Login");
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogin model)
        {
            if (!ModelState.IsValid) return View(model);

            var client = _clientFactory.CreateClient();
            var apiUrl = _config["ApiSettings:BaseUrl"] + "/api/auth/login";

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                //ModelState.AddModelError("", "Invalid login attempt");
                // return View(model);
                ViewBag.Error = "Invalid username or password.";
                return View(model);
            }

            var result = await response.Content.ReadAsStringAsync();
            var tokenObj = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

            HttpContext.Session.SetString("JWToken", tokenObj["token"]);

            return RedirectToAction("Index", "Student");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("JWToken"); // clear the token
            return RedirectToAction("Index", "Home");      // go back to Home page
        }

    }
}

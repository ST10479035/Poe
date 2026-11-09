using Microsoft.AspNetCore.Mvc;
using RaceDayAPI3.Models;
using RaceDay.MVC.Services;

namespace RaceDay.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApiService _apiService;
        public AccountController(ApiService apiService) => _apiService = apiService;

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7030/"); // Match your RaceDayAPI port

            var response = await client.PostAsJsonAsync("api/auth/login", model);
            if (response.IsSuccessStatusCode)
            {
                var loginResult = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
                if (loginResult != null && !string.IsNullOrEmpty(loginResult.Token))
                {
                    HttpContext.Session.SetString("JWToken", loginResult.Token);
                    HttpContext.Session.SetString("UserEmail", model.Email);
                    HttpContext.Session.SetString("UserRole", loginResult.Role);

                    if (loginResult.Role == "Organiser")
                    {
                        return RedirectToAction("Dashboard", "Organiser");
                    }
                    return RedirectToAction("Index", "Participant");
                }
            }

            ModelState.AddModelError("", "Invalid email or password.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clears session tracking on logout
            return RedirectToAction("Login", "Account"); // Redirects securely back to Login page
        }
    }
}
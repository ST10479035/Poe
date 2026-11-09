using Microsoft.AspNetCore.Mvc;
using RaceDayAPI3.Models;
using RaceDay.MVC.Services;

namespace RaceDay.MVC.Controllers
{
    public class OrganiserController : Controller
    {
        private readonly ApiService _apiService;
        public OrganiserController(ApiService apiService) => _apiService = apiService;

        private bool IsOrganiser() => HttpContext.Session.GetString("UserRole") == "Organiser";

        public async Task<IActionResult> Dashboard()
        {
            if (!IsOrganiser()) return RedirectToAction("Login", "Account");
            var events = await _apiService.GetAsync<List<EventViewModel>>("api/organiser/events") ?? new();
            return View(events);
        }

        [HttpGet]
        public async Task<IActionResult> CreateEvent()
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            List<CategoryViewModel> categories = new();

            try
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7030/"); // Verify your backend API port
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync("api/categories");
                if (response.IsSuccessStatusCode)
                {
                    categories = await response.Content.ReadFromJsonAsync<List<CategoryViewModel>>() ?? new();
                }
            }
            catch
            {
                // Fallback categories guarantees dropdown populates for testing
                categories = new List<CategoryViewModel>
        {
            new CategoryViewModel { Id = 1, CategoryName = "Running" },
            new CategoryViewModel { Id = 2, CategoryName = "Cycling" },
            new CategoryViewModel { Id = 3, CategoryName = "Triathlon" },
            new CategoryViewModel { Id = 4, CategoryName = "Trail Run" }
        };
            }

            ViewBag.Categories = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(categories, "Id", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEvent(EventViewModel model, IFormFile? imageFile)
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            // Optional: Uncomment to bypass strict ModelState validation if properties mismatch during testing
            // ModelState.Clear();

            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7030/"); // Match your backend API port
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            // Build multipart form data content to send text fields and file together
            using var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(model.Title ?? string.Empty), "Title");
            formData.Add(new StringContent(model.Description ?? string.Empty), "Description");
            formData.Add(new StringContent(model.EventDate.ToString("yyyy-MM-ddTHH:mm:ss")), "EventDate");
            formData.Add(new StringContent(model.Location ?? string.Empty), "Location");
            formData.Add(new StringContent(model.Distance.ToString()), "Distance");
            formData.Add(new StringContent(model.CategoryId.ToString()), "CategoryId");

            if (imageFile != null && imageFile.Length > 0)
            {
                var streamContent = new StreamContent(imageFile.OpenReadStream());
                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(imageFile.ContentType);
                formData.Add(streamContent, "imageFile", imageFile.FileName);
            }

            var response = await client.PostAsync("api/events", formData);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Dashboard");
            }

            // If API submission fails, re-populate categories dropdown so the view renders correctly
            try
            {
                var catResponse = await client.GetAsync("api/categories");
                if (catResponse.IsSuccessStatusCode)
                {
                    var categories = await catResponse.Content.ReadFromJsonAsync<List<CategoryViewModel>>() ?? new();
                    ViewBag.Categories = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(categories, "Id", "CategoryName", model.CategoryId);
                }
            }
            catch
            {
                ViewBag.Categories = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(new List<CategoryViewModel>(), "Id", "CategoryName");
            }

            ModelState.AddModelError("", "Failed to save event to the database. Please check all fields.");
            return View(model);
        }
    }
}
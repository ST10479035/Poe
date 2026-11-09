using Microsoft.AspNetCore.Mvc;
using RaceDayAPI3.Models;
using RaceDay.MVC.Services;

namespace RaceDay.MVC.Controllers
{
    public class ParticipantController : Controller
    {
        private readonly ApiService _apiService;
        public ParticipantController(ApiService apiService) => _apiService = apiService;

        private bool IsParticipant() => HttpContext.Session.GetString("UserRole") == "Participant";

        public async Task<IActionResult> Index()
        {
            if (!IsParticipant()) return RedirectToAction("Login", "Account");
            var events = await _apiService.GetAsync<List<EventViewModel>>("api/events/upcoming") ?? new();
            return View(events);
        }
 
        public async Task<IActionResult> Details(int id)
        {
            if (!IsParticipant()) return RedirectToAction("Login", "Account");
            var eventItem = await _apiService.GetAsync<EventViewModel>($"api/events/{id}");
            return View(eventItem);
        }

        [HttpPost]
        public async Task<IActionResult> Enrol(int eventId, int categoryId)
        {
            if (!IsParticipant()) return RedirectToAction("Login", "Account");
            await _apiService.PostAsync("api/enrolments", new { EventId = eventId, CategoryId = categoryId });
            return RedirectToAction(nameof(MyEvents));
        }

        public async Task<IActionResult> MyEvents()
        {
            if (!IsParticipant()) return RedirectToAction("Login", "Account");
            var enrolments = await _apiService.GetAsync<List<EnrolmentViewModel>>("api/enrolments/my-events") ?? new();
            return View(enrolments);
        }

        public async Task<IActionResult> MyResults()
        {
            if (!IsParticipant()) return RedirectToAction("Login", "Account");
            var results = await _apiService.GetAsync<List<EnrolmentViewModel>>("api/enrolments/my-results") ?? new();
            return View(results);
        }

        public async Task<IActionResult> Profile()
        {
            if (!IsParticipant()) return RedirectToAction("Login", "Account");
            var profile = await _apiService.GetAsync<UserProfileViewModel>("api/participant/profile") ?? new();
            return View(profile);
        }
    }
}

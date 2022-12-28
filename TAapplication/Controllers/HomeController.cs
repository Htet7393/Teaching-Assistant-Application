using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TAapplication.Models;
using Microsoft.AspNetCore.Authorization;

namespace TAapplication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View("Index");
        }

        [Authorize(Roles = "Administrator, Professor")]
        public IActionResult ApplicationList()
        {
            return View();
        }

        [Authorize(Roles = "Applicant")]
        public IActionResult ApplicationCreate()
        {
            return View();
        }

        [Authorize(Roles = "Administrator, Professor")]
        public IActionResult ApplicationDetails()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
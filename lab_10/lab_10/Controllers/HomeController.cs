using System.Diagnostics;
using lab_10.Models; // Poprawiony namespace
using Microsoft.AspNetCore.Mvc;

namespace lab_10.Controllers // Poprawiony namespace
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
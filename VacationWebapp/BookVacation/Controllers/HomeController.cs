using BookVacation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using VacationApp.Services.Core.Interface;
using VacationApp.ViewModels.Vacation;

namespace BookVacation.Controllers
{
    public class HomeController :BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly   IVacationService vacationService;
        private readonly UserManager<IdentityUser?>? UserManager;

        public HomeController(ILogger<HomeController> logger, IVacationService vacService, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            this.vacationService = vacService;
            this.UserManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {

                return View();
                // return RedirectToAction("Index","Vacation");
            }

            var user = await  UserManager.FindByIdAsync(userId);


              ViewBag.EmailConfirmed = user?.EmailConfirmed ?? false;

            return View();
        }

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

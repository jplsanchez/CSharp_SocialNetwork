using LoginPageMVC.Models;
using LoginPageMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Diagnostics;

namespace LoginPageMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAction(LoginModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var loginClient = RestService.For<IUserApiService>("https://localhost:7174");

                    var response = await loginClient.LoginUserAsync(user);

                    return RedirectToAction("Index", "Home");
                }
                return View("Login");
            }
            catch (Refit.ApiException ex)
            {
                TempData["ErrorMessage"] = $"Falha ao realizar o Login. {ex.ReasonPhrase}";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to Login: {message}", ex.Message);
                TempData["ErrorMessage"] = $"Erro ao realizar o Login";
                return RedirectToAction("Login");
            }
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
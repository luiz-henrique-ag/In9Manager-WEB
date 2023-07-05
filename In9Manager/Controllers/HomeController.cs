using In9Manager.Helpers.Session;
using In9Manager.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace In9Manager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISessao _session;
        public HomeController(ILogger<HomeController> logger, ISessao sessao)
        {
            _logger = logger;
            _session = sessao;
        }

        public IActionResult Index()
        {
            if (_session.GetSession() == null) return RedirectToAction(nameof(AuthController.Login), "Auth");
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
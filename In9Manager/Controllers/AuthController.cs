using In9Manager.Data;
using In9Manager.Helpers.HashGenerator;
using In9Manager.Helpers.Session;
using In9Manager.Models;
using In9Manager.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace In9Manager.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationContext db;
        private readonly ISessao _session;

        public AuthController(ApplicationContext _db, ISessao session)
        {
            db = _db;
            _session = session;
        }
        public IActionResult Login()
        {
            if(_session.GetSession() != null) return RedirectToAction(nameof(HomeController.Index), "Home");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(UsuarioViewModel model)
        {
            var usuario = await db.Usuarios.FirstOrDefaultAsync(m => m.Login == model.Login);
            if(usuario == null || model.Senha.GenerateHash() != usuario.Senha)
            {
                TempData["Erro"] = "Usuário e/ou senha incorretos.";
            }
            else
            {
                _session.NewSession(usuario);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View(nameof(Login));
        }

        public IActionResult Logout()
        {
            if(_session.GetSession() == null) return View(nameof(Login));
            _session.RemoveSession();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}

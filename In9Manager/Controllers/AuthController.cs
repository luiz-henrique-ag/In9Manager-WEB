using In9Manager.Data;
using In9Manager.Helpers.HashGenerator;
using In9Manager.Helpers.Session;
using In9Manager.Models;
using In9Manager.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignIn(UsuarioViewModel model)
        {
            var usuario = db.Usuarios.FirstOrDefault(m => m.Login == model.Login);
            if(usuario == null || model.Senha.GenerateHash() == usuario.Senha)
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using In9Manager.Data;
using In9Manager.Models;
using Microsoft.Extensions.Logging.Console;
using In9Manager.Helpers.HashGenerator;

namespace In9Manager.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ApplicationContext db;

        public UsuariosController(ApplicationContext context)
        {
            db = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            return db.Usuarios != null ?
                        View(await db.Usuarios.ToListAsync()) :
                        Problem("Entity set 'ApplicationContext.Usuarios'  is null.");
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await db.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuario usuario, string confirmarSenha)
        {
            if(usuario.Senha != confirmarSenha)
            {
                TempData["Erro"] = "Senhas devem ser iguais";
                return View(usuario);
            }
            if (ModelState.IsValid)
            {
                usuario.Senha = usuario.Senha.GenerateHash();
                db.Usuarios.Add(usuario);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await db.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Login,Cpf,Senha,Permissao")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(usuario);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await db.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Usuarios == null)
            {
                return Problem("Entity set 'ApplicationContext.Usuarios'  is null.");
            }
            var usuario = await db.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                db.Usuarios.Remove(usuario);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return (db.Usuarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

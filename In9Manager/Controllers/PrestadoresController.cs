using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using In9Manager.Data;
using In9Manager.Models;
using In9Manager.Helpers.Session;

namespace In9Manager.Controllers
{
    public class PrestadoresController : Controller
    {
        private readonly ApplicationContext db;
        private readonly ISessao _session;
        public PrestadoresController(ApplicationContext context, ISessao session)
        {
            db = context;
            _session = session;
        }

        // GET: Prestadores
        public async Task<IActionResult> Index()
        {
            if (_session.GetSession() == null) return RedirectToAction(nameof(AuthController.Login), "Auth");
            return db.Prestadores != null ? 
                          View(await db.Prestadores.ToListAsync()) :
                          Problem("Entity set 'ApplicationContext.Prestadores'  is null.");
        }

        // GET: Prestadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (_session.GetSession() == null) return RedirectToAction(nameof(AuthController.Login), "Auth");
            if (id == null || db.Prestadores == null)
            {
                return NotFound();
            }

            var prestador = await db.Prestadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prestador == null)
            {
                return NotFound();
            }

            return View(prestador);
        }

        // GET: Prestadores/Create
        public IActionResult Create()
        {
            if (_session.GetSession() == null) return RedirectToAction(nameof(AuthController.Login), "Auth");
            return View();
        }

        // POST: Prestadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,CPF,TipoPrestador,Telefone")] Prestador prestador)
        {
            if (ModelState.IsValid)
            {
                db.Add(prestador);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prestador);
        }

        // GET: Prestadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (_session.GetSession() == null) return RedirectToAction(nameof(AuthController.Login), "Auth");
            if (id == null || db.Prestadores == null)
            {
                return NotFound();
            }

            var prestador = await db.Prestadores.FindAsync(id);
            if (prestador == null)
            {
                return NotFound();
            }
            return View(prestador);
        }

        // POST: Prestadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,CPF,TipoPrestador,Telefone")] Prestador prestador)
        {
            if (id != prestador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(prestador);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrestadorExists(prestador.Id))
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
            return View(prestador);
        }

        // GET: Prestadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (_session.GetSession() == null) return RedirectToAction(nameof(AuthController.Login), "Auth");
            if (id == null || db.Prestadores == null)
            {
                return NotFound();
            }

            var prestador = await db.Prestadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prestador == null)
            {
                return NotFound();
            }

            return View(prestador);
        }

        // POST: Prestadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Prestadores == null)
            {
                return Problem("Entity set 'ApplicationContext.Prestadores'  is null.");
            }
            var prestador = await db.Prestadores.FindAsync(id);
            if (prestador != null)
            {
                db.Prestadores.Remove(prestador);
            }
            
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrestadorExists(int id)
        {
          return (db.Prestadores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

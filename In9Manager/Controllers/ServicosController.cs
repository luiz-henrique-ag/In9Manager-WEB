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
    public class ServicosController : Controller
    {
        private readonly ApplicationContext db;
        private readonly ISessao _session;
        public ServicosController(ApplicationContext context, ISessao session)
        {
            db = context;
            _session = session;
        }

        // GET: Servicos
        public async Task<IActionResult> Index()
        {
            if (_session.GetSession() == null) return RedirectToAction(nameof(AuthController.Login), "Auth");
            return db.Servicos != null ? 
                          View(await db.Servicos.ToListAsync()) :
                          Problem("Entity set 'ApplicationContext.Servicos'  is null.");
        }

        // GET: Servicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (_session.GetSession() == null) return RedirectToAction(nameof(AuthController.Login), "Auth");
            if (id == null || db.Servicos == null)
            {
                return NotFound();
            }

            var servico = await db.Servicos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (servico == null)
            {
                return NotFound();
            }

            return View(servico);
        }

        // GET: Servicos/Create
        public IActionResult Create()
        {
            if (_session.GetSession() == null) return RedirectToAction(nameof(AuthController.Login), "Auth");
            CarregaMaoObra();
            TempData["Preco"] = "0,0";
            return View();
        }

        // POST: Servicos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Servico servico, string preco)
        {
            servico.Valor = decimal.Parse(preco.Remove(0, 2));
            
            if (ModelState.IsValid)
            {
                db.Servicos.Add(servico);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            CarregaMaoObra();
            TempData["Preco"] = preco;
            return View(servico);
        }

        // GET: Servicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (_session.GetSession() == null) return RedirectToAction(nameof(AuthController.Login), "Auth");
            if (id == null || db.Servicos == null)
            {
                return NotFound();
            }

            var servico = await db.Servicos.FindAsync(id);
            if (servico == null)
            {
                return NotFound();
            }
            CarregaMaoObra();
            return View(servico);
        }

        // POST: Servicos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Servico servico)
        {
            if (id != servico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(servico);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicoExists(servico.Id))
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
            CarregaMaoObra();
            return View(servico);
        }

        // GET: Servicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (_session.GetSession() == null) return RedirectToAction(nameof(AuthController.Login), "Auth");
            if (id == null || db.Servicos == null)
            {
                return NotFound();
            }

            var servico = await db.Servicos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (servico == null)
            {
                return NotFound();
            }

            return View(servico);
        }

        // POST: Servicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Servicos == null)
            {
                return Problem("Entity set 'ApplicationContext.Servicos'  is null.");
            }
            var servico = await db.Servicos.FindAsync(id);
            if (servico != null)
            {
                db.Servicos.Remove(servico);
            }
            
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicoExists(int id)
        {
          return (db.Servicos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private void CarregaMaoObra()
        {
            IList<object> list = new List<object>();
            foreach (var item in (Enum.GetValues(typeof(TipoMaodeObra))))
            {
                list.Add(new
                {
                    Id = (int)item,
                    Nome = item
                });
            }
            SelectList tipo = new SelectList(list, "Id", "Nome");
            ViewData["MaoObra"] = tipo;
        }
    }
}

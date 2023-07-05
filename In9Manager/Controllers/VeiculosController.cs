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
    public class VeiculosController : Controller
    {
        private readonly ApplicationContext db;
        private readonly ISessao _session;
        public VeiculosController(ApplicationContext context, ISessao session)
        {
            db = context;
            _session = session;
        }

        // GET: Veiculos
        public async Task<IActionResult> Index()
        {
            if (_session.GetSession() == null) return RedirectToAction(nameof(AuthController.Login), "Auth");
            var applicationContext = db.Veiculos.Include(v => v.Cliente);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Veiculos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (_session.GetSession() == null) return RedirectToAction(nameof(AuthController.Login), "Auth");
            if (id == null || db.Veiculos == null)
            {
                return NotFound();
            }

            var veiculo = await db.Veiculos
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veiculo == null)
            {
                return NotFound();
            }

            return View(veiculo);
        }

        // GET: Veiculos/Create
        public IActionResult Create()
        {
            if (_session.GetSession() == null) return RedirectToAction(nameof(AuthController.Login), "Auth");
            return View();
        }

        // POST: Veiculos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Veiculo veiculo, int ClienteId)
        {
            if(ClienteId == 0)
            {
                ModelState.AddModelError("", "Um Erro Ocorreu");
                return View(veiculo);
            }
            veiculo.ClienteID = ClienteId;
            if (ModelState.IsValid)
            {
                db.Veiculos.Add(veiculo);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(veiculo);
        }

        // GET: Veiculos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (_session.GetSession() == null) return RedirectToAction(nameof(AuthController.Login), "Auth");
            if (id == null || db.Veiculos == null)
            {
                return NotFound();
            }

            var veiculo = await db.Veiculos.Include(x => x.Cliente).FirstOrDefaultAsync(x=> x.Id == id);
            if (veiculo == null)
            {
                return NotFound();
            }
            return View(veiculo);
        }

        // POST: Veiculos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Veiculo veiculo, int ClienteId)
        {
            if (id != veiculo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                veiculo.ClienteID = ClienteId;
                try
                {
                    db.Update(veiculo);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeiculoExists(veiculo.Id))
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
            return View(veiculo);
        }

        // GET: Veiculos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (_session.GetSession() == null) return RedirectToAction(nameof(AuthController.Login), "Auth");
            if (id == null || db.Veiculos == null)
            {
                return NotFound();
            }

            var veiculo = await db.Veiculos
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veiculo == null)
            {
                return NotFound();
            }

            return View(veiculo);
        }

        // POST: Veiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Veiculos == null)
            {
                return Problem("Entity set 'ApplicationContext.Veiculos'  is null.");
            }
            var veiculo = await db.Veiculos.FindAsync(id);
            if (veiculo != null)
            {
                db.Veiculos.Remove(veiculo);
            }
            
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VeiculoExists(int id)
        {
          return (db.Veiculos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using In9Manager.Data;
using In9Manager.Models;

namespace In9Manager.Controllers
{
    public class VeiculosController : Controller
    {
        private readonly ApplicationContext db;

        public VeiculosController(ApplicationContext context)
        {
            db = context;
        }

        // GET: Veiculos
        public async Task<IActionResult> Index()
        {
            var applicationContext = db.Veiculos.Include(v => v.Cliente);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Veiculos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
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
            return View();
        }

        // POST: Veiculos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Veiculo veiculo, int ClienteId)
        {
            if (ModelState.IsValid)
            {
                db.Veiculos.Add(veiculo);
                return RedirectToAction(nameof(Index));
            }
            return View(veiculo);
        }

        // GET: Veiculos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Veiculos == null)
            {
                return NotFound();
            }

            var veiculo = await db.Veiculos.FindAsync(id);
            if (veiculo == null)
            {
                return NotFound();
            }
            ViewData["ClienteID"] = new SelectList(db.Cliente, "Id", "CPF", veiculo.ClienteID);
            return View(veiculo);
        }

        // POST: Veiculos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Modelo,Marca,Ano,Categoria,Placa,ClienteID")] Veiculo veiculo)
        {
            if (id != veiculo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
            ViewData["ClienteID"] = new SelectList(db.Cliente, "Id", "CPF", veiculo.ClienteID);
            return View(veiculo);
        }

        // GET: Veiculos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
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

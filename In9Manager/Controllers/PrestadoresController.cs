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
    public class PrestadoresController : Controller
    {
        private readonly ApplicationContext _context;

        public PrestadoresController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Prestadores
        public async Task<IActionResult> Index()
        {
              return _context.Prestadores != null ? 
                          View(await _context.Prestadores.ToListAsync()) :
                          Problem("Entity set 'ApplicationContext.Prestadores'  is null.");
        }

        // GET: Prestadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Prestadores == null)
            {
                return NotFound();
            }

            var prestador = await _context.Prestadores
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
                _context.Add(prestador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prestador);
        }

        // GET: Prestadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Prestadores == null)
            {
                return NotFound();
            }

            var prestador = await _context.Prestadores.FindAsync(id);
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
                    _context.Update(prestador);
                    await _context.SaveChangesAsync();
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
            if (id == null || _context.Prestadores == null)
            {
                return NotFound();
            }

            var prestador = await _context.Prestadores
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
            if (_context.Prestadores == null)
            {
                return Problem("Entity set 'ApplicationContext.Prestadores'  is null.");
            }
            var prestador = await _context.Prestadores.FindAsync(id);
            if (prestador != null)
            {
                _context.Prestadores.Remove(prestador);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrestadorExists(int id)
        {
          return (_context.Prestadores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using In9Manager.Data;
using In9Manager.Models;
using In9Manager.Models.ViewModels;
using In9Manager.Helpers.Session;

namespace In9Manager.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ApplicationContext db;
        private readonly ISessao _session;

        public ClientesController(ApplicationContext context, ISessao session)
        {
            db = context;
            _session = session;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            if (_session.GetSession() == null) return RedirectToAction(nameof(AuthController.Login), "Auth");
            return View(await db.Cliente.ToListAsync());  
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (_session.GetSession() == null) return RedirectToAction(nameof(AuthController.Login), "Auth");
            if (id == null || db.Cliente == null)
            {
                return NotFound();
            }

            var cliente = await db.Cliente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            if (_session.GetSession() == null) return RedirectToAction(nameof(AuthController.Login), "Auth");
            ClienteViewModel model = new ClienteViewModel();
            return View(model);
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClienteViewModel model)
        {
            Cliente cliente = model.Cliente;
            ClienteEndereco endereco = model.ClienteEndereco;
            if (ModelState.IsValid)
            {
                db.Cliente.Add(cliente);
                await db.SaveChangesAsync();
                endereco.ClienteId = cliente.Id;
                db.ClienteEndereco.Add(endereco);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "Ocorreu um erro");
            }
            return View(model);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (_session.GetSession() == null) return RedirectToAction(nameof(AuthController.Login), "Auth");
            if (id == null || db.Cliente == null)
            {
                return NotFound();
            }

            var cliente = await db.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,CPF,Telefone,DataNascimento")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(cliente);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
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
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.Cliente == null)
            {
                return NotFound();
            }

            var cliente = await db.Cliente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Cliente == null)
            {
                return Problem("Entity set 'ApplicationContext.Cliente'  is null.");
            }
            var cliente = await db.Cliente.FindAsync(id);
            if (cliente != null)
            {
                db.Cliente.Remove(cliente);
            }
            
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
          return (db.Cliente?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

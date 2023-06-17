using System;
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
            //if (_session.GetSession() == null) return RedirectToAction(nameof(AuthController.Login), "Auth");
            return View(await db.Cliente.ToListAsync());  
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //if (_session.GetSession() == null) return RedirectToAction(nameof(AuthController.Login), "Auth");
            if (id == null || db.Cliente == null)
            {
                return NotFound();
            }
            var model = new ClienteViewModel();
            var endereco = await db.ClienteEndereco.Where(x => x.ClienteId == id).FirstOrDefaultAsync();
            var cliente = await db.Cliente
                .FirstOrDefaultAsync(m => m.Id == id);
            var veiculos = await db.Veiculos.Where(x => x.ClienteID == id).ToListAsync();
            if (cliente == null || endereco == null || veiculos == null)
            {
                return NotFound();
            }
            model.Cliente = cliente;
            model.ClienteEndereco = endereco;
            model.Veiculos = veiculos;
            return View(model);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            //if (_session.GetSession() == null) return RedirectToAction(nameof(AuthController.Login), "Auth");
            ClienteViewModel model = new ClienteViewModel();
            return View(model);
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClienteViewModel model, string dataNascimento)
        {
            Cliente cliente = model.Cliente;
            ClienteEndereco endereco = model.ClienteEndereco;
            cliente.DataNascimento = DateTime.Parse(dataNascimento);
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
                return View(model);
            }
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //if (_session.GetSession() == null) return RedirectToAction(nameof(AuthController.Login), "Auth");
            if (id == null || db.Cliente == null)
            {
                return NotFound();
            }
            var model = new ClienteViewModel();
            var cliente = await db.Cliente.FirstOrDefaultAsync(x => x.Id == id);
            var endereco = await db.ClienteEndereco.Where(x => x.ClienteId == id).FirstOrDefaultAsync();
            if (cliente == null || endereco == null)
            {
                return NotFound();
            }
            model.ClienteEndereco = endereco;
            model.Cliente = cliente;

            return View(model);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClienteViewModel model, string dataNascimento)
        {
            if (id != model.Cliente.Id)
            {
                return NotFound();
            }
            model.Cliente.DataNascimento = DateTime.Parse(dataNascimento);
            if (ModelState.IsValid)
            {
                model.ClienteEndereco.ClienteId = model.Cliente.Id;
                try
                {
                    db.Cliente.Update(model.Cliente);
                    db.ClienteEndereco.Update(model.ClienteEndereco);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(model.Cliente.Id) || !EnderecoExists(model.ClienteEndereco.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Erro");
                        return View(model);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.Cliente == null || db.ClienteEndereco == null)
            {
                return NotFound();
            }
            var model = new ClienteViewModel();
            var endereco = await db.ClienteEndereco.Where(x => x.ClienteId == id).FirstOrDefaultAsync();
            var cliente = await db.Cliente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null || endereco == null)
            {
                return NotFound();
            }
            model.Cliente = cliente;
            model.ClienteEndereco = endereco;
            return View(model);
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
            var endereco = await db.ClienteEndereco.FirstOrDefaultAsync(m => m.ClienteId == id);
            if (cliente != null || endereco != null)
            {
                db.Cliente.Remove(cliente);
                db.ClienteEndereco.Remove(endereco);
            }
            
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return (db.Cliente?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private bool EnderecoExists(int id)
        {
            return (db.ClienteEndereco?.Any(x => x.Id == id)).GetValueOrDefault();
        }
    }
}

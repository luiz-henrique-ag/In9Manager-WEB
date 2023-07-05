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
            if (_session.GetSession() == null) return RedirectToAction(nameof(AuthController.Login), "Auth");
            return View(await db.Clientes.ToListAsync());  
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (_session.GetSession() == null) return RedirectToAction(nameof(AuthController.Login), "Auth");
            if (id == null || db.Clientes == null)
            {
                return NotFound();
            }
            var model = new ClienteViewModel();
            var endereco = await db.ClienteEnderecos.Where(x => x.ClienteId == id).FirstOrDefaultAsync();
            var cliente = await db.Clientes
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
            if (_session.GetSession() == null) return RedirectToAction(nameof(AuthController.Login), "Auth");
            ClienteViewModel model = new ClienteViewModel();
            TempData["data"] = DateTime.Now.ToShortDateString();
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
                db.Clientes.Add(cliente);
                await db.SaveChangesAsync();
                endereco.ClienteId = cliente.Id;
                db.ClienteEnderecos.Add(endereco);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["data"] = dataNascimento;
                ModelState.AddModelError("", "Ocorreu um erro");
                return View(model);
            }
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (_session.GetSession() == null) return RedirectToAction(nameof(AuthController.Login), "Auth");
            if (id == null || db.Clientes == null)
            {
                return NotFound();
            }
            var model = new ClienteViewModel();
            var cliente = await db.Clientes.FirstOrDefaultAsync(x => x.Id == id);
            var endereco = await db.ClienteEnderecos.Where(x => x.ClienteId == id).FirstOrDefaultAsync();
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
                    var cliente = model.Cliente;
                    db.Update(cliente);
                    db.Update(model.ClienteEndereco);
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
                        TempData["data"] = dataNascimento;
                        return View(model);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            TempData["data"] = dataNascimento;
            return View(model);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (_session.GetSession() == null) return RedirectToAction(nameof(AuthController.Login), "Auth");
            if (id == null || db.Clientes == null || db.ClienteEnderecos == null)
            {
                return NotFound();
            }
            var model = new ClienteViewModel();
            var endereco = await db.ClienteEnderecos.Where(x => x.ClienteId == id).FirstOrDefaultAsync();
            var cliente = await db.Clientes
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
            if (db.Clientes == null)
            {
                return Problem("Entity set 'ApplicationContext.Cliente'  is null.");
            }
            var cliente = await db.Clientes.FindAsync(id);
            var endereco = await db.ClienteEnderecos.FirstOrDefaultAsync(m => m.ClienteId == id);
            if (cliente != null || endereco != null)
            {
                db.Clientes.Remove(cliente);
                db.ClienteEnderecos.Remove(endereco);
            }
            
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return (db.Clientes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private bool EnderecoExists(int id)
        {
            return (db.ClienteEnderecos?.Any(x => x.Id == id)).GetValueOrDefault();
        }
    }
}

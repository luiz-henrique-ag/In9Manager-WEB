using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using In9Manager.Data;
using In9Manager.Models;
using System.Security.Cryptography.Xml;
using In9Manager.Helpers.Session;

namespace In9Manager.Controllers
{
    public class OrcamentosController : Controller
    {
        private readonly ApplicationContext db;
        private readonly ISessao _session;

        public OrcamentosController(ApplicationContext context, ISessao session)
        {
            db = context;
            _session = session;
        }

        // GET: Orcamentos
        public async Task<IActionResult> Index()
        {
            if (_session.GetSession() == null) return RedirectToAction(nameof(AuthController.Login), "Auth");
            var applicationContext = db.Orcamentos.Include(o => o.Cliente);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Orcamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (_session.GetSession() == null) return RedirectToAction(nameof(AuthController.Login), "Auth");
            if (id == null || db.Orcamentos == null)
            {
                return NotFound();
            }

            var orcamento = await db.Orcamentos
                .Include(o => o.Cliente)
                .Include(x => x.Servicos)
                .ThenInclude(x => x.Servico)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (orcamento == null)
            {
                return NotFound();
            }

            return View(orcamento);
        }

        // GET: Orcamentos/Create
        public IActionResult Create()
        {
            if (_session.GetSession() == null) return RedirectToAction(nameof(AuthController.Login), "Auth");
            CarregaFormaPagamento();
            CarregaStatus();
            TempData["DataCriacao"] = DateTime.Now.ToShortDateString();
            return View();
        }

        // POST: Orcamentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Orcamento orcamento, List<int> Servicos, string DataCriacao, string DataValidade,
            string Placa, int ClienteId, double Desconto, decimal ValorTotal, decimal ValorFinal)
        {
            if (ModelState.IsValid)
            {
                orcamento.PlacaVeiculo = Placa;
                orcamento.DataCriacao = DateTime.Parse(DataCriacao);
                orcamento.DataValidade = DateTime.Parse(DataValidade);
                orcamento.ClienteId = ClienteId;
                orcamento.Desconto = Desconto;
                orcamento.ValorFinal = ValorFinal;
                orcamento.ValorTotal = ValorTotal;
                db.Orcamentos.Add(orcamento);
                try
                {
                    await db.SaveChangesAsync();
                    Servicos.ForEach(x =>
                    {
                        var servico = new OrcamentoServico();
                        servico.ServicoId = x;
                        servico.OrcamentoId = orcamento.Id;
                        db.OrcamentoServicos.Add(servico);
                    });
                    await db.SaveChangesAsync();
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Erro inesperado.");
                    return View(orcamento);
                }
                return RedirectToAction(nameof(Index));
            }
            TempData["DataCriacao"] = DataCriacao;
            TempData["DataValidade"] = DataValidade;
            CarregaStatus();
            CarregaFormaPagamento();
            return View(orcamento);
        }

        // GET: Orcamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (_session.GetSession() == null) return RedirectToAction(nameof(AuthController.Login), "Auth");
            if (id == null || db.Orcamentos == null)
            {
                return NotFound();
            }

            var orcamento = await db.Orcamentos.FindAsync(id);
            if (orcamento == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(db.Clientes, "Id", "CPF", orcamento.ClienteId);
            return View(orcamento);
        }

        // POST: Orcamentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClienteId,DataCriacao,DataValidade,ValorTotal,PlacaVeiculo,Desconto,ValorFinal,Status,FormaPagamento,NumeroParcelas")] Orcamento orcamento)
        {
            if (id != orcamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(orcamento);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrcamentoExists(orcamento.Id))
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
            ViewData["ClienteId"] = new SelectList(db.Clientes, "Id", "CPF", orcamento.ClienteId);
            return View(orcamento);
        }

        // GET: Orcamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (_session.GetSession() == null) return RedirectToAction(nameof(AuthController.Login), "Auth");
            if (id == null || db.Orcamentos == null)
            {
                return NotFound();
            }

            var orcamento = await db.Orcamentos
                .Include(o => o.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orcamento == null)
            {
                return NotFound();
            }

            return View(orcamento);
        }

        // POST: Orcamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Orcamentos == null)
            {
                return Problem("Entity set 'ApplicationContext.Orcamentos'  is null.");
            }
            var orcamento = await db.Orcamentos.FindAsync(id);
            if (orcamento != null)
            {
                db.Orcamentos.Remove(orcamento);
            }
            
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrcamentoExists(int id)
        {
          return (db.Orcamentos?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private void CarregaFormaPagamento()
        {
            IList<object> list = new List<object>();
            foreach (var item in (Enum.GetValues(typeof(OrcamentoFormaPagamento))))
            {
                list.Add(new
                {
                    Id = (int)item,
                    Nome = item
                });
            }
            SelectList forma = new SelectList(list, "Id", "Nome");
            ViewData["FormaPagamento"] = forma;
        }
        private void CarregaStatus()
        {
            IList<object> list = new List<object>();
            foreach (var item in Enum.GetValues(typeof(OrcamentoStatus)))
            {
                list.Add(new
                {
                    Id = (int)item,
                    Nome = item
                });
            }
            SelectList status = new SelectList(list, "Id", "Nome");
            ViewData["Status"] = status;
        }
    }
}

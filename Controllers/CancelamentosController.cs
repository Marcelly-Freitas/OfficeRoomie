using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using OfficeRoomie.Database;
using OfficeRoomie.Models;

namespace OfficeRoomie.Controllers
{
    public class CancelamentosController : Controller
    {
        private readonly AppDbContext _context;

        public CancelamentosController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dados = await _context.Cancelamentos.ToListAsync();

            return View(dados);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cancelamento cancelamento)
        {
            if (ModelState.IsValid)
            {
                _context.Cancelamentos.Add(cancelamento);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }

            return View();
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var dados = await _context.Cancelamentos.FindAsync(id);

            if (dados == null)
                return NotFound();

            return View(dados);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Cancelamento cancelamento)
        {
            if (id != cancelamento.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Cancelamentos.Update(cancelamento);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }

            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
                    return NotFound();

            var dados = await _context.Cancelamentos.FindAsync(id);
                     
            if(dados == null)
                    return NotFound();

            return View(dados);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var dados = await _context.Cancelamentos.FindAsync(id);

            if (dados == null)
                return NotFound();

            return View(dados);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
                return NotFound();

            var dados = await _context.Cancelamentos.FindAsync(id);

            if (dados == null)
                return NotFound();

            _context.Cancelamentos.Remove(dados);
            await _context.SaveChangesAsync();


            return RedirectToAction("Index");

        }
    }
}

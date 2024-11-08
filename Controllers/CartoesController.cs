using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeRoomie.Database;
using OfficeRoomie.Models;


namespace OfficeRoomie.Controllers;

[Authorize]
public class CartoesController : Controller
{
    private readonly AppDbContext _context;

    public CartoesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Cartoes
    public async Task<IActionResult> Index()
    {
        return View(await _context.Cartoes
            .OrderByDescending(c => c.numeroDoCartao)
            .ToListAsync());
    }

    // GET: Cartoes/Details/numeroDoCartao
    public async Task<IActionResult> Details(string numeroDoCartao)
    {
        if (string.IsNullOrEmpty(numeroDoCartao))
        {
            return NotFound();
        }

        var cartao = await _context.Cartoes
            .FirstOrDefaultAsync(c => c.numeroDoCartao == numeroDoCartao);
        if (cartao == null)
        {
            return NotFound();
        }

        return View(cartao);
    }

    // GET: Cartoes/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Cartoes/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("numeroDoCartao,nomeDoTitular,validade,cvv")] Cartao cartao)
    {
        if (ModelState.IsValid)
        {
            _context.Add(cartao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(cartao);
    }

    // GET: Cartoes/Edit/numeroDoCartao
    public async Task<IActionResult> Edit(string numeroDoCartao)
    {
        if (string.IsNullOrEmpty(numeroDoCartao))
        {
            return NotFound();
        }

        var cartao = await _context.Cartoes
            .FirstOrDefaultAsync(c => c.numeroDoCartao == numeroDoCartao);
        if (cartao == null)
        {
            return NotFound();
        }

        return View(cartao);
    }

    // POST: Cartoes/Edit/numeroDoCartao
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string numeroDoCartao, [Bind("numeroDoCartao,nomeDoTitular,validade,cvv")] Cartao cartao)
    {
        if (numeroDoCartao != cartao.numeroDoCartao)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(cartao);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartaoExists(cartao.numeroDoCartao))
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
        return View(cartao);
    }

    // GET: Cartoes/Delete/numeroDoCartao
    public async Task<IActionResult> Delete(string numeroDoCartao)
    {
        if (string.IsNullOrEmpty(numeroDoCartao))
        {
            return NotFound();
        }

        var cartao = await _context.Cartoes
            .FirstOrDefaultAsync(c => c.numeroDoCartao == numeroDoCartao);
        if (cartao == null)
        {
            return NotFound();
        }

        return View(cartao);
    }

    // POST: Cartoes/Delete/numeroDoCartao
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string numeroDoCartao)
    {
        var cartao = await _context.Cartoes
            .FirstOrDefaultAsync(c => c.numeroDoCartao == numeroDoCartao);
        if (cartao != null)
        {
            _context.Cartoes.Remove(cartao);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private bool CartaoExists(string numeroDoCartao)
    {
        return _context.Cartoes.Any(c => c.numeroDoCartao == numeroDoCartao);
    }
}

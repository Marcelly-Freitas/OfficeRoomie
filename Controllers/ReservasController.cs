using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeRoomie.Database;
using OfficeRoomie.Helpers;
using OfficeRoomie.Models;
using OfficeRoomie.Models.ViewModels;
using OfficeRoomie.Services;

namespace OfficeRoomie.Controllers
{
    public class ReservasController : Controller
    {
        private readonly AppDbContext _context;
        private readonly EmailService _emailService;

        public ReservasController(AppDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<IActionResult> Index(int? pageNumber, string searchString, string currentFilter)
        {
            if (searchString != null) {
                pageNumber = 1;
            } else {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var reservas = _context.Reservas
                .AsNoTracking()
                .OrderByDescending(a => a.id)
                .Include(r => r.cliente)
                .Include(r => r.sala); ;
            var reservasFiltradas = !String.IsNullOrEmpty(searchString)
                ? reservas.Where(s => s.protocolo.ToLower().Contains(searchString.ToLower()))
                : reservas;
            var reservasPaginadas = await ModelPaginado<Reserva>.CreateAsync(reservasFiltradas, pageNumber ?? 1, 5);

            return View(reservasPaginadas);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva
                .Include(r => r.cliente)
                .Include(r => r.sala)
                .FirstOrDefaultAsync(m => m.id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        async public Task<IActionResult> Create()
        {
            var salas = await _context.Salas.ToListAsync();
            var clientes = await _context.Clientes.ToListAsync();

            var viewModel = new ReservaCreate
            {
                reserva = new Reserva(),
                salas = salas,
                clientes = clientes
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ReservaCreate dto)
        {
            if (ModelState.IsValid)
            {
                var reserva = new Reserva
                {
                    hora_inicio = dto.reserva.hora_inicio,
                    hora_fim = dto.reserva.hora_fim,
                    data_reserva = dto.reserva.data_reserva,
                    status = dto.reserva.status,
                    cliente_id = dto.reserva.cliente_id,
                    sala_id = dto.reserva.sala_id,
                    protocolo = ProtocoloHelper.GerarProtocolo(),
                };

                _context.Add(reserva);
                await _context.SaveChangesAsync();

                // await _emailService.SendEmailAsync("destinatario@exemplo.com", "Assunto do Email", "<p>Conteúdo do email</p>");

                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }

            var salas = await _context.Salas.ToListAsync();
            var clientes = await _context.Clientes.ToListAsync();

            var viewModel = new ReservaCreate
            {
                reserva = reserva,
                salas = salas,
                clientes = clientes
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] ReservaCreate dto)
        {
            if (ModelState.IsValid)
            {
                var reserva = await _context.Reserva
                    .Include(r => r.cliente)
                    .Include(r => r.sala)
                    .FirstOrDefaultAsync(m => m.id == id);

                if (reserva == null)
                {
                    return NotFound();
                }

                reserva.hora_inicio = dto.reserva.hora_inicio;
                reserva.hora_fim = dto.reserva.hora_fim;
                reserva.data_reserva = dto.reserva.data_reserva;
                reserva.status = dto.reserva.status;

                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();

                    // Se houver cliente smtp configurado, chama essa função pra notificar por email
                    // await _emailService.SendEmailAsync(reserva.cliente!.email, "OfficeRoomie: Sua reserva foi atualizada", "<p>Conteúdo do email</p>");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.id))
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
            return View(dto);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva
                .Include(r => r.cliente)
                .Include(r => r.sala)
                .FirstOrDefaultAsync(m => m.id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva != null)
            {
                _context.Reserva.Remove(reserva);
            }

            await _context.SaveChangesAsync();

            // await _emailService.SendEmailAsync("destinatario@exemplo.com", "Assunto do Email", "<p>Conteúdo do email</p>");

            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(int id)
        {
            return _context.Reserva.Any(e => e.id == id);
        }
    }
}

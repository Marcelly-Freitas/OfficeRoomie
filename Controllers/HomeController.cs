using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Datatypes;
using MySqlX.XDevAPI;
using OfficeRoomie.Database;
using OfficeRoomie.Helpers;
using OfficeRoomie.Models;
using OfficeRoomie.Models.ViewModels;
using System.Diagnostics;


namespace OfficeRoomie.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    async public Task<IActionResult> Index(int? pageNumber)
    {
        var salas = _context.Salas.OrderByDescending(a => a.id);
        var salasPaginados = await ModelPaginado<Sala>.CreateAsync(salas, pageNumber ?? 1, 12);

        return View(salasPaginados);
    }

    public async Task<IActionResult> ClienteReserva(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var sala = await _context.Salas
            .FirstOrDefaultAsync(m => m.id == id);
        if (sala == null)
        {
            return NotFound();
        }

        var viewModel = new ClienteReserva
        {
            reserva = new Reserva(),
            sala = sala,
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> ClienteReserva(int id, ClienteReserva dto)
    {
        var cliente = new Cliente
        {
            nome = dto.cliente.nome,
            email = dto.cliente.email,
        };

        await _context.Clientes.AddAsync(cliente);
        await _context.SaveChangesAsync();

        var reserva = new Reserva
        {
            hora_inicio = dto.reserva.hora_inicio,
            hora_fim = dto.reserva.hora_fim,
            data_reserva = dto.reserva.data_reserva,
            status = "solicitada",
            cliente_id = cliente.id,
            sala_id = id,
            protocolo = ProtocoloHelper.GerarProtocolo(),
        };

        await _context.AddAsync(reserva);
        await _context.SaveChangesAsync();

        TempData["MensagemSucesso"] = "Reserva realizada com sucesso! - " + reserva.protocolo;

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Sobre()
    {
        return View();
    }

    public IActionResult Ajuda()
    {
        return View();
    }

    public IActionResult Privacidade()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

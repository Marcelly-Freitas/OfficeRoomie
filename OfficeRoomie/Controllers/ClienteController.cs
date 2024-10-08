using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using OfficeRoomie.Repositories;
using OfficeRoomie.Models;
using OfficeRoomie.ViewModels;
using OfficeRoomie.DTO;

namespace OfficeRoomie.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/cliente")]
public class ClienteController : Controller
{
    private readonly IClienteRepository _clienteRepository;

    public ClienteController(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<ClienteViewModel>> GetClientes()
    {
        var clientes = await _clienteRepository.GetClientesAsync();

        var viewModel = clientes.Select(u => new ClienteViewModel
        {
            id = u.id,
            nome = u.nome,
            email = u.email,
        });

        return viewModel;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCliente([FromRoute] int id)
    {
        var cliente = await _clienteRepository.GetClienteByIdAsync(id);

        if (cliente == null)
        {
            return NotFound();
        }

        var viewModel = new ClienteViewModel
        {
            id = cliente.id,
            nome = cliente.nome,
            email = cliente.email,
        };

        return Ok(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> StoreCliente([FromBody] ClienteDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var cliente = new Cliente
        {
            nome = dto.nome,
            email = dto.email,
        };

        await _clienteRepository.AddClienteAsync(cliente);

        return CreatedAtAction("GetCliente", new { id = cliente.id }, cliente);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCliente([FromRoute] int id)
    {
        var Cliente = await _clienteRepository.DeleteClienteAsync(id);

        if (Cliente == null)
        {
            return NotFound();
        }

        return Ok(Cliente);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCliente([FromRoute] int id, [FromBody] ClienteDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var cliente = new Cliente
        {
            nome = dto.nome,
            email = dto.email,
        };

        var updatedCliente = await _clienteRepository.UpdateClienteAsync(id, cliente);

        if (updatedCliente == null)
        {
            return NotFound();
        }

        return Ok(updatedCliente);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdatePatchCliente([FromRoute] int id, [FromBody] JsonPatchDocument ClienteDocument)
    {
        var updatedCliente = await _clienteRepository.UpdateClientePatchAsync(id, ClienteDocument);

        if (updatedCliente == null)
        {
            return NotFound();
        }

        return Ok(updatedCliente);
    }
}
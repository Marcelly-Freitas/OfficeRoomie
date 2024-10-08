using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using OfficeRoomie.Helpers;
using OfficeRoomie.Repositories;
using OfficeRoomie.Models;
using OfficeRoomie.DTO;
using OfficeRoomie.ViewModels;

namespace OfficeRoomie.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/administrador")]
public class AdministradorController : Controller
{
    private readonly IAdministradorRepository _administradorRepository;

    public AdministradorController(IAdministradorRepository administradorRepository)
    {
        _administradorRepository = administradorRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<AdministradorViewModel>> GetAdministradores()
    {
        var Administradores = await _administradorRepository.GetAdministradoresAsync();
        var viewModel = Administradores.Select(u => new AdministradorViewModel
        {
            id = u.id,
            nome = u.nome,
            email = u.email,
            permissoes = u.permissoes,
        });

        return viewModel;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAdministrador([FromRoute] int id)
    {
        var Administrador = await _administradorRepository.GetAdministradorByIdAsync(id);

        if (Administrador == null)
        {
            return NotFound();
        }

        var viewModel = new AdministradorViewModel
        {
            id = Administrador.id,
            nome = Administrador.nome,
            email = Administrador.email,
            permissoes = Administrador.permissoes,
        };

        return Ok(viewModel);
    }


    [HttpPost]
    public async Task<IActionResult> StoreAdministrador([FromBody] AdministradorDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var administrador = new Administrador
        {
            id = dto.id,
            nome = dto.nome,
            email = dto.email,
            senha = dto.senha,
            permissoes = dto.permissoes,
        };

        administrador.senha = PasswordHelper.HashPassword(administrador.senha);

        await _administradorRepository.AddAdministradorAsync(administrador);

        return CreatedAtAction("GetAdministrador", new { id = administrador.id }, administrador);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAdministrador([FromRoute] int id)
    {
        var administrador = await _administradorRepository.DeleteAdministradorAsync(id);

        if (administrador == null)
        {
            return NotFound();
        }

        return Ok(administrador);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAdministrador([FromRoute] int id, [FromBody] AdministradorDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var administrador = new Administrador
        {
            id = dto.id,
            nome = dto.nome,
            email = dto.email,
            senha = dto.senha,
            permissoes = dto.permissoes,
        };

        var updatedAdministrador = await _administradorRepository.UpdateAdministradorAsync(id, administrador);

        if (updatedAdministrador == null)
        {
            return NotFound();
        }

        return Ok(updatedAdministrador);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdatePatchAdministrador([FromRoute] int id, [FromBody] JsonPatchDocument AdministradorDocument)
    {
        var updatedAdministrador = await _administradorRepository.UpdateAdministradorPatchAsync(id, AdministradorDocument);

        if (updatedAdministrador == null)
        {
            return NotFound();
        }

        return Ok(updatedAdministrador);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using OfficeRoomie.Models;
using OfficeRoomie.Database;

namespace OfficeRoomie.Repositories;
public class AdministradorRepository : IAdministradorRepository
{
    private readonly AppDbContext _dbContext;

    public AdministradorRepository(AppDbContext dBContext)
    {
        _dbContext = dBContext;
    }

    public async Task<IEnumerable<Administrador>> GetAdministradoresAsync()
    {
        var administradores = await _dbContext.Administradores.AsNoTracking().ToListAsync();
        return administradores;
    }

    public async Task<Administrador?> GetAdministradorByIdAsync(int id)
    {
        var administrador = await _dbContext.Administradores.AsNoTracking().FirstOrDefaultAsync(x => x.id == id);
        return administrador;
    }

    public async Task<Administrador?> AddAdministradorAsync(Administrador Administrador)
    {
        try
        {
            await _dbContext.Administradores.AddAsync(Administrador);
            await _dbContext.SaveChangesAsync();
            return Administrador;
        }
        catch (Exception)
        {
            throw new Exception("Falha ao cadstrar usuario");
        }
    }

    public async Task<Administrador?> DeleteAdministradorAsync(int id)
    {
        var Administrador = await GetAdministradorByIdAsync(id);

        if (Administrador == null)
        {
            return Administrador;
        }

        _dbContext.Administradores.Remove(Administrador);
        await _dbContext.SaveChangesAsync();

        return Administrador;
    }

    public async Task<Administrador?> UpdateAdministradorAsync(int id, Administrador administrador)
    {
        var administradorQuery = await GetAdministradorByIdAsync(id);

        if (administradorQuery == null)
        {
            return administradorQuery;
        }

        _dbContext.Entry(administradorQuery).CurrentValues.SetValues(administrador);
        await _dbContext.SaveChangesAsync();

        return administradorQuery;
    }

    public async Task<Administrador?> UpdateAdministradorPatchAsync(int id, JsonPatchDocument administradorDocument)
    {
        var AdministradorQuery = await GetAdministradorByIdAsync(id);

        if (AdministradorQuery == null)
        {
            return AdministradorQuery;
        }

        administradorDocument.ApplyTo(AdministradorQuery);
        await _dbContext.SaveChangesAsync();

        return AdministradorQuery;
    }
}
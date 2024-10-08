using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using OfficeRoomie.Models;
using OfficeRoomie.Database;

namespace OfficeRoomie.Repositories;
public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _dbContext;

    public ClienteRepository(AppDbContext dBContext)
    {
        _dbContext = dBContext;
    }

    public async Task<IEnumerable<Cliente>> GetClientesAsync()
    {
        var clientes = await _dbContext.Clientes.AsNoTracking().ToListAsync();
        return clientes;
    }

    public async Task<Cliente?> GetClienteByIdAsync(int id)
    {
        var cliente = await _dbContext.Clientes.AsNoTracking().FirstOrDefaultAsync(x => x.id == id);
        return cliente;
    }

    public async Task<Cliente?> AddClienteAsync(Cliente cliente)
    {
        try
        {
            await _dbContext.Clientes.AddAsync(cliente);
            await _dbContext.SaveChangesAsync();

            return cliente;
        }
        catch (Exception)
        {
            throw new Exception("Falha ao cadstrar usuario");
        }
    }

    public async Task<Cliente?> DeleteClienteAsync(int id)
    {
        var cliente = await GetClienteByIdAsync(id);

        if (cliente == null)
        {
            return cliente;
        }

        _dbContext.Clientes.Remove(cliente);
        await _dbContext.SaveChangesAsync();

        return cliente;
    }

    public async Task<Cliente?> UpdateClienteAsync(int id, Cliente cliente)
    {
        var clienteQuery = await GetClienteByIdAsync(id);

        if (clienteQuery == null)
        {
            return clienteQuery;
        }

        _dbContext.Entry(clienteQuery).CurrentValues.SetValues(cliente);
        await _dbContext.SaveChangesAsync();

        return clienteQuery;
    }

    public async Task<Cliente?> UpdateClientePatchAsync(int id, JsonPatchDocument clienteDocument)
    {
        var clienteQuery = await GetClienteByIdAsync(id);

        if (clienteQuery == null)
        {
            return clienteQuery;
        }

        clienteDocument.ApplyTo(clienteQuery);
        await _dbContext.SaveChangesAsync();

        return clienteQuery;
    }
}
using Microsoft.AspNetCore.JsonPatch;
using OfficeRoomie.Models;

namespace OfficeRoomie.Repositories;
public interface IClienteRepository
{
    Task<IEnumerable<Cliente>> GetClientesAsync();

    Task<Cliente?> GetClienteByIdAsync(int id);

    Task<Cliente?> AddClienteAsync(Cliente Cliente);

    Task<Cliente?> DeleteClienteAsync(int id);

    Task<Cliente?> UpdateClienteAsync(int id, Cliente cliente);

    Task<Cliente?> UpdateClientePatchAsync(int id, JsonPatchDocument cliente);
}
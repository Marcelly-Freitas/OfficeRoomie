using Microsoft.AspNetCore.JsonPatch;
using OfficeRoomie.Models;

namespace OfficeRoomie.Repositories
{
    public interface IAdministradorRepository
    {
        Task<IEnumerable<Administrador>> GetAdministradoresAsync();

        Task<Administrador?> GetAdministradorByIdAsync(int id);

        Task<Administrador?> AddAdministradorAsync(Administrador Administrador);

        Task<Administrador?> DeleteAdministradorAsync(int id);

        Task<Administrador?> UpdateAdministradorAsync(int id, Administrador administrador);

        Task<Administrador?> UpdateAdministradorPatchAsync(int id, JsonPatchDocument Administrador);
    }
}
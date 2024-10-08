using OfficeRoomie.Models;

namespace OfficeRoomie.Repositories;
public interface IAuthRepository
{
    Task<Administrador?> ValidateAdministradorAsync(string email, string password);
    string CreateToken(Administrador administrador);
}
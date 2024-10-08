using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using OfficeRoomie.Security;
using OfficeRoomie.Helpers;
using OfficeRoomie.Models;
using OfficeRoomie.Database;
using System.Text;

namespace OfficeRoomie.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly AppDbContext _dbContext;

    public AuthRepository(AppDbContext dBContext)
    {
        _dbContext = dBContext;
    }

    public async Task<Administrador?> ValidateAdministradorAsync(string email, string password)
    {
        var administrador = await _dbContext.Administradores.FirstOrDefaultAsync(x => x.email == email);

        if (administrador == null || !PasswordHelper.VerifyPassword(password, administrador.senha))
        {
            return null;
        }

        return administrador;
    }

    public string CreateToken(Administrador administrador)
    {
        var handler = new JwtSecurityTokenHandler();

        var privateKey = Encoding.UTF8.GetBytes(SecurityConfiguration.PrivateKey);

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(privateKey),
            SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = "yourIssuer",
            Audience = "yourAudience",
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddHours(1),
            Subject = GenerateClaims(administrador)
        };

        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaims(Administrador administrador)
    {
        var ci = new ClaimsIdentity();

        ci.AddClaim(new Claim("id", administrador.id.ToString()));
        ci.AddClaim(new Claim(ClaimTypes.Name, administrador.nome));
        ci.AddClaim(new Claim(ClaimTypes.GivenName, administrador.nome));
        ci.AddClaim(new Claim(ClaimTypes.Email, administrador.email));

        foreach (var role in administrador.permissoes)
        {
            ci.AddClaim(new Claim(ClaimTypes.Role, role));
        }

        return ci;
    }
}
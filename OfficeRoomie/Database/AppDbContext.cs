using Microsoft.EntityFrameworkCore;
using OfficeRoomie.Models;

namespace OfficeRoomie.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Administrador> Administradores { get; set; }
    }
}

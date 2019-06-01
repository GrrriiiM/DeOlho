using DeOlho.Services.Politicos.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace DeOlho.Services.Politicos.Api.Infrastructure.Data
{
    public class DeOlhoDbContext : DbContext
    {
        public DeOlhoDbContext(DbContextOptions options)
            : base(options)
        {
            
        }
        public DbSet<Mandato> Mandatos { get; set; }
        public DbSet<Partido> Partidos { get; set; }
        public DbSet<Politico> Politicos { get; set; }
    }
}
using DeOlho.ETL.dadosabertos_camara_leg_br.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace DeOlho.ETL.dadosabertos_camara_leg_br.Api.Infrastructure.Data
{
    public class DeOlhoDbContext : DbContext
    {
        public DeOlhoDbContext(DbContextOptions options)
            : base(options)
        {
            
        }
        public DbSet<Legislatura> Legislaturas { get; set; }
        public DbSet<Partido> Partidos { get; set; }
        public DbSet<Politico> Politicos { get; set; }
        public DbSet<Despesa> Despesas { get; set; }
    }
}
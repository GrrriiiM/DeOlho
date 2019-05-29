using DeOlho.ETL.dadosabertos_camara_leg_br.Api.Domains;
using Microsoft.EntityFrameworkCore;

namespace DeOlho.ETL.dadosabertos_camara_leg_br.Api.Infra.Data
{
    public class DeOlhoDbContext : DbContext
    {
        public DeOlhoDbContext(DbContextOptions options)
            : base(options)
        {
            
        }
        public DbSet<Legislatura> Legislaturas { get; set; }
    }
}
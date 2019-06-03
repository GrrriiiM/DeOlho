using DeOlho.ETL.tse_jus_br.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace DeOlho.ETL.tse_jus_br.Api.Infrastructure.Data
{
    public class DeOlhoDbContext : DbContext
    {
        public DeOlhoDbContext(DbContextOptions options)
            : base(options)
        {
            
        }

        public DbSet<Politico> Politicos { get; set; }
    }
}
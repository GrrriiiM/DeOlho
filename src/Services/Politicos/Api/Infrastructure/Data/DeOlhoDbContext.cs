using DeOlho.Services.Politicos.Api.Domain;
using DeOlho.Services.Politicos.Api.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace DeOlho.Services.Politicos.Api.Infrastructure.Data
{
    public class DeOlhoDbContext : DbContext
    {
        public DeOlhoDbContext(DbContextOptions options)
            : base(options)
        {
            
        }
        public DbSet<Politico> Politicos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Politico>(entity => {
                
            });

            modelBuilder.Entity<PoliticoEscolaridade>()
                .HasData(Enumeration.GetAll<PoliticoEscolaridade>());

            modelBuilder.Entity<PoliticoSexo>()
                .HasData(Enumeration.GetAll<PoliticoSexo>());

            modelBuilder.Entity<PoliticoSituacao>()
                .HasData(Enumeration.GetAll<PoliticoSituacao>());

            modelBuilder.Entity<MandatoTipo>()
                .HasData(Enumeration.GetAll<MandatoTipo>());

            modelBuilder.Entity<MandatoSituacao>()
                .HasData(Enumeration.GetAll<MandatoSituacao>());
        }
    }
}
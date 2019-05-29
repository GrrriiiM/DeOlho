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

        public DbSet<Legislatura> Legislaturas { get; set; }
    }
}
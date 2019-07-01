using System;
using System.Threading;
using System.Threading.Tasks;
using DeOlho.Services.Despesas.Domain;
using DeOlho.Services.Despesas.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeOlho.Services.Despesas.Infrastucture.Data
{
    public class DeOlhoDbContext: DbContext, IUnitOfWork
    {

        private readonly IMediator _mediator;

        private DeOlhoDbContext(DbContextOptions<DeOlhoDbContext> options) : base(options) { }

        public DeOlhoDbContext(DbContextOptions<DeOlhoDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public override int SaveChanges()
        {
            _mediator.DispatchDomainEventsAsync(this).Wait();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            _mediator.DispatchDomainEventsAsync(this).Wait();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _mediator.DispatchDomainEventsAsync(this);
            return await base.SaveChangesAsync(cancellationToken);
        }

        public async override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _mediator.DispatchDomainEventsAsync(this);
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PoliticoAnoMesTipo>(build => {
                build.HasKey(_ => new { _.CPF, _.Mes, _.Ano, _.TipoId });
            });

            modelBuilder.Entity<PoliticoAnoTipo>(build => {
                build.HasKey(_ => new { _.CPF, _.Ano, _.TipoId });
            });

            modelBuilder.Entity<PoliticoMes>(build => {
                build.HasKey(_ => new { _.CPF, _.Mes, _.Ano });
            });

            modelBuilder.Entity<PoliticoAno>(build => {
                build.HasKey(_ => new { _.CPF, _.Ano });
            });

            modelBuilder.Entity<TotalAno>(build => {
                build.HasKey(_ => new { _.Ano });
            });

            modelBuilder.Entity<TotalAnoMes>(build => {
                build.HasKey(_ => new { _.Mes, _.Ano });
            });

            modelBuilder.Entity<TotalPolitico>(build => {
                build.HasKey(_ => new { _.CPF });
            });

            modelBuilder.Entity<TotalTipo>(build => {
                build.HasKey(_ => new { _.TipoId });
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
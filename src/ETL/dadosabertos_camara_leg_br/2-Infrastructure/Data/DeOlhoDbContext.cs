using System;
using System.Threading;
using System.Threading.Tasks;
using DeOlho.ETL.dadosabertos_leg_br.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeOlho.ETL.dadosabertos_leg_br.Infrastructure.Data
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
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeputadoFederal>(build => {
                build.HasKey(_ => _.Id);
                build.HasIndex(_ => _.CPF).IsUnique();
                build.HasIndex(_ => _.OrigemId).IsUnique();
            });

            modelBuilder.Entity<NotaFiscal>(build => {
                build.HasKey(_ => _.Id);
                build.HasIndex(_ => _.CodDocumento).IsUnique();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
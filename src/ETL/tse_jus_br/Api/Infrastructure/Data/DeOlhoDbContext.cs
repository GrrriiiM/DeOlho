using System;
using System.Threading.Tasks;
using DeOlho.ETL.tse_jus_br.Api.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DeOlho.ETL.tse_jus_br.Api.Infrastructure.Data
{
    public class DeOlhoDbContext : DbContext
    {

        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;

        private DeOlhoDbContext(DbContextOptions<DeOlhoDbContext> options) : base(options) { }

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;

        public DeOlhoDbContext(DbContextOptions<DeOlhoDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public DbSet<Politico> Politicos { get; set; }

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

        public async override Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await _mediator.DispatchDomainEventsAsync(this);
            return await base.SaveChangesAsync(cancellationToken);
        }

        public async override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await _mediator.DispatchDomainEventsAsync(this);
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
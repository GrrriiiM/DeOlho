using System.Linq;
using System.Threading.Tasks;
using DeOlho.ETL.tse_jus_br.Api.Domain;
using DeOlho.ETL.tse_jus_br.Api.Domain.SeedWork;
using DeOlho.ETL.tse_jus_br.Api.Infrastructure.Data;
using MediatR;

namespace DeOlho.ETL.tse_jus_br.Api.Infrastructure
{
    static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, DeOlhoDbContext deOlhoDbContext)
        {
            var domainEntities = deOlhoDbContext.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.GetDomainEvents().Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.GetDomainEvents())
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) => {
                    await mediator.Publish(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}
using System.Linq;
using System.Threading.Tasks;
using DeOlho.Services.Despesas.Domain.SeedWork;
using DeOlho.Services.Despesas.Infrastucture.Data;
using MediatR;

namespace DeOlho.Services.Despesas.Infrastructure.Data
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
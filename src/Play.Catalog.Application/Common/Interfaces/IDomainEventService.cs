using Play.Catalog.Domain.Common;

namespace Play.Catalog.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
 
}
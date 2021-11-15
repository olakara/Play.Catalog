using Play.Catalog.Domain.Entities;

namespace Play.Catalog.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        Task<IList<Item>> GetAllAsync();

        Task<Item> GetAsync(Guid id);

        Task CreateAsync(Item item);

        Task UpdateAsync(Item item);

        Task DeleteAsync(Guid id);
    }
}
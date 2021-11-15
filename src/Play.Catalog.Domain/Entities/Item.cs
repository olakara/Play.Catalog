using Play.Catalog.Domain.Common;

namespace Play.Catalog.Domain.Entities
{
    public class Item : AuditableEntity
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
     
    }
}
namespace Play.Catalog.Domain.Common
{
    public abstract class AuditableEntity 
    {
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
using Play.Catalog.Application.Common.Interfaces;

namespace Play.Catalog.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
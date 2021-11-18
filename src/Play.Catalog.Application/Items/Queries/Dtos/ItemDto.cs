using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Play.Catalog.Application.Items.Queries.Dtos
{

    public record ItemDto(Guid Id, string Name,string Description, decimal Price, DateTimeOffset CreatedDate);
}

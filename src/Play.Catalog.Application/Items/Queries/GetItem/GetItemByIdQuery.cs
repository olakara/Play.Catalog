using MediatR;
using Play.Catalog.Application.Common.Interfaces;
using Play.Catalog.Application.Items.Queries.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Play.Catalog.Application.Items.Queries.GetItem
{
    public class GetItemByIdQuery : IRequest<ItemDto>
    {
        public Guid Id { get; set; }
    }



    public class GetItemByIdQueryHandler : IRequestHandler<GetItemByIdQuery, ItemDto>
    {
        private readonly IApplicationDbContext _context;

        public GetItemByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ItemDto> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.GetAsync(request.Id);

            return new ItemDto(result.Id, result.Name, result.Description, result.Price, result.CreatedDate);
        }
    }
}

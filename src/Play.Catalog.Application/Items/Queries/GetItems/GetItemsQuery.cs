using AutoMapper;
using MediatR;
using Play.Catalog.Application.Common.Interfaces;
using Play.Catalog.Application.Items.Queries.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Play.Catalog.Application.Items.Queries.GetItems
{
    public class GetItemsQuery : IRequest<IList<ItemDto>>
    {

    }

    public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, IList<ItemDto>>
    {
        private readonly IApplicationDbContext _context;       

        public GetItemsQueryHandler(IApplicationDbContext context)
        {
            this._context = context;            
        }
        public async Task<IList<ItemDto>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.GetAllAsync();

            return result.Select(x => new ItemDto(x.Id, x.Name, x.Description, x.Price, x.CreatedDate)).ToList();
        }
    }
}

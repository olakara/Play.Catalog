using MediatR;
using Play.Catalog.Application.Common.Interfaces;
using Play.Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Play.Catalog.Application.Items.Commands.UpdateItem
{
    public class UpdateItemCommand : IRequest
    {
        public Guid Id { get; set; }    
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            await _context.UpdateAsync(new Item
            {
                Id = request.Id,
                Description = request.Description,
                Name = request.Name,
                Price = request.Price
            });

            return Unit.Value;
        }
    }
}

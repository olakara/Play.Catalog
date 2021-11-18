using MediatR;
using Play.Catalog.Application.Common.Interfaces;
using Play.Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Play.Catalog.Application.Items.Commands.CreateItem
{
    public class CreateItemCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(CreateItemCommand command, CancellationToken cancellationToken)
        {
            var id = Guid.NewGuid();
            await _context.CreateAsync(new Item
            {
             Id = id,
             CreatedBy = "System",
             CreatedDate = DateTime.Now,
             Description = command.Description,
             Name = command.Name,
             Price = command.Price             
            });

            return id;
        }
    }
}

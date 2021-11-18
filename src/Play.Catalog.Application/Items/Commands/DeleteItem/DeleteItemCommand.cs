using MediatR;
using Play.Catalog.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Play.Catalog.Application.Items.Commands.DeleteItem
{
    public class DeleteItemCommand : IRequest
    {
        public Guid Id { get; set; }    
    }

    public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            await _context.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}

using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Items
{
    public class GetItemQueryHandler : IRequestHandler<GetItemQuery, OperationResult<object>>
    {
        private readonly IItemRepository _itemRepository;

        public GetItemQueryHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<OperationResult<object>> Handle(GetItemQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();
            var item = await _itemRepository.GetByIdAsync(request.Id);

            result.Value = new
            {
                Id = item.Id,
                InvoiceId= item.InvoiceId,
                Name = item.Name,
                Description = item.Description,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice

            };
            
            return result;
        }
    }
}

using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Items
{
    public class SaveItemCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}

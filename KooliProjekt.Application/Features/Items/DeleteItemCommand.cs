using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Items
{
    public class DeleteItemCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
    }
}

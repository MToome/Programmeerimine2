using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Application.Features.Items
{
    [ExcludeFromCodeCoverage]
    public class DeleteItemCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
    }
}

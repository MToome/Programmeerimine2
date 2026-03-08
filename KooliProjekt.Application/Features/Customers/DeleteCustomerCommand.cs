using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Application.Features.Customers
{
    [ExcludeFromCodeCoverage]

    // 15.11
    // Customeri kustutamise käsk
    public class DeleteCustomerCommand : IRequest<OperationResult>, ITransactional
    {

        public int Id { get; set; }


        }
}

using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Customers
{

    // 15.11
    // Customeri kustutamise käsk
    public class DeleteCustomerCommand : IRequest<OperationResult>, ITransactional
    {

        public int Id { get; set; }


        }
}

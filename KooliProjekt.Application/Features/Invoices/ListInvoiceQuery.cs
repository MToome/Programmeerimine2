using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System.Collections.Generic;

namespace KooliProjekt.Application.Features.Invoices
{
    public class ListInvoiceQuery : IRequest<OperationResult<IList<Invoice>>>
    {
    }
}

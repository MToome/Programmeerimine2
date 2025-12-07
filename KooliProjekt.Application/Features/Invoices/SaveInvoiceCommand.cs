using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Invoices
{
    public class SaveInvoiceCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public int CustomerId { get; set; }
        public string Customer { get; set; }
        public List<int> Items { get; set; }
    }
}

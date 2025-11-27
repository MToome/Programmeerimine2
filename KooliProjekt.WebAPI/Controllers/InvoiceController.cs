using KooliProjekt.Application.Features.Invoices;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KooliProjekt.WebAPI.Controllers
{
    public class InvoiceController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public InvoiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] ListInvoiceQuery query)
        {
            var result = await _mediator.Send(query);

            return Result(result);
        }

        // API Pöördumispunkt Invoice kustutamiseks
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(DeleteInvoiceCommand command)
        {
            var response = await _mediator.Send(command);

            return Result(response);
        }

    }
}

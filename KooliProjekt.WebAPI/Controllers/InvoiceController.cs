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
        [Route("List")]
        public async Task<IActionResult> List([FromQuery] ListInvoiceQuery query)
        {
            var response = await _mediator.Send(query);

            return Result(response);
        }

        // API Pöördumispunkt ühe Invoice toomiseks ID alusel
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetInvoiceQuery { Id = id };
            var response = await _mediator.Send(query);

            return Result(response);
        }

        // API Pöördumispunkt Invoice salvestamiseks
        [HttpPost]
        [Route("Save")]

        public async Task<IActionResult> Save([FromBody] SaveInvoiceCommand command)
        {
            var response = await _mediator.Send(command);
            return Result(response);

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

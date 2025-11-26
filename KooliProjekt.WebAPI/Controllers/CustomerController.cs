using KooliProjekt.Application.Features.Customers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KooliProjekt.WebAPI.Controllers
{
    public class CustomerController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] ListCustomerQuery query)
        {
            var result = await _mediator.Send(query);

            return Result(result);
        }


        // API Pöördumispunkt Customeri kustutamiseks0
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(DeleteCustomerCommand command)
        {
            var response = await _mediator.Send(command);

            return Result(response);
        }
    }
}

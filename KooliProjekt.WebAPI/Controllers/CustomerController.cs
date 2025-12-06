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
        [Route("List")]
        public async Task<IActionResult> List([FromQuery] ListCustomerQuery query)
        {
            var response = await _mediator.Send(query);

            return Result(response);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get( int id)
        {
            var query = new GetCustomerQuery  {Id = id};
            var response = await _mediator.Send(query);

            return Result(response);
        }




        // API Pöördumispunkt Customeri kustutamiseks
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(DeleteCustomerCommand command)
        {
            var response = await _mediator.Send(command);

            return Result(response);
        }
    }
}

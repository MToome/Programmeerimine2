using KooliProjekt.Application.Features.Items;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KooliProjekt.WebAPI.Controllers
{
    public class ItemController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public ItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] ListItemQuery query)
        {
            var result = await _mediator.Send(query);

            return Result(result);
        }

        // API Pöördumispunkt Item kustutamiseks
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(DeleteItemCommand command)
        {
            var response = await _mediator.Send(command);
            return Result(response);
        }
    }
}

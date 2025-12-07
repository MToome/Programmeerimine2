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
        [Route("List")]
        public async Task<IActionResult> List([FromQuery] ListItemQuery query)
        {
            var response = await _mediator.Send(query);

            return Result(response);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetItemQuery { Id = id };
            var response = await _mediator.Send(query);

            return Result(response);
        }

        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save(SaveItemCommand command)
        {
            var response = await _mediator.Send(command);
            return Result(response);
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


using KooliProjekt.Application.Features.Invoices;
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
    }
}

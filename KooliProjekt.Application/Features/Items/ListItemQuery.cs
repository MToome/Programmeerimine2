using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System.Collections.Generic;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Features.Items
{
    public class ListItemQuery : IRequest<OperationResult<IList<Item>>>
    {
    }
}

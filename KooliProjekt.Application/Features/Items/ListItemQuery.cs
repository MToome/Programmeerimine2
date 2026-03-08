using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Application.Features.Items
{
    [ExcludeFromCodeCoverage]
    public class ListItemQuery : IRequest<OperationResult<PagedResult<Item>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Name { get; set; }
    }
}

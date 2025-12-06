using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Items

{
    public class GetItemQuery : IRequest<OperationResult<object>>
    {
        public int Id { get; set; }
    }
}

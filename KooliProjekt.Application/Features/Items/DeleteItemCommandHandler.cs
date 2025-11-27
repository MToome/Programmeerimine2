using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Items
{
    public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteItemCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            // Kustutamine üle relatsioonide (CASCADE DELETE)
            await _dbContext
                .Items
                .Where(c => c.Id == request.Id)
                .ExecuteDeleteAsync();

            return result;
        }
    }
}

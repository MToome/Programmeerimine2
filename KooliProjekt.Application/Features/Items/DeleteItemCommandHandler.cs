using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = new OperationResult();

            if (request.Id <= 0)
            {
                return result;
            }
            // Kustutamine üle relatsioonide (CASCADE DELETE)
            var item = await _dbContext
                .Items
                .Where(c => c.Id == request.Id)
                .FirstOrDefaultAsync(t => t.Id == request.Id);

            if (item == null)
            {
                return result;
            }

            _dbContext.Items.Remove(item);

            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}

using FluentValidation;
using KooliProjekt.Application.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Items
{
    public class SaveItemCommandValidator : AbstractValidator<SaveItemCommand>
    {
        public SaveItemCommandValidator(ApplicationDbContext context)
        {
            RuleFor(i => i.Name)
                .NotEmpty().WithMessage("Item name is required.")
                .MaximumLength(100).WithMessage("Item name cannot exceed 100 characters.");
            RuleFor(i => i.UnitPrice)
                .GreaterThan(0).WithMessage("Item price must be greater than zero.");
            RuleFor(i => i.Quantity)
                .GreaterThan(0).WithMessage("Item quantity must be greater than zero.");
            RuleFor(i => i.Description)
                .MaximumLength(500).WithMessage("Item description cannot exceed 500 characters.");
            RuleFor(i => i.InvoiceId)
                .MustAsync(async (invoiceId, cancellation) =>
                {
                    var invoice = await context.Invoices.FindAsync(new object[] { invoiceId }, cancellation);
                    return invoice != null;
                })
                .WithMessage("Associated invoice must exist in the database.");
        }
    }
}

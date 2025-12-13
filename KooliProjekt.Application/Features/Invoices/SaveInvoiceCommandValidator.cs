using FluentValidation;
using System;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Features.Invoices
{
    public class SaveInvoiceCommandValidator : AbstractValidator<SaveInvoiceCommand>
    {
        public SaveInvoiceCommandValidator(ApplicationDbContext context) 
        { 
            RuleFor(i => i.Items)
                .NotEmpty().WithMessage("Invoice must have at least one item.");
            RuleFor(i => i.DueDate)
                .GreaterThan(DateTime.Now).WithMessage("Due date must be in the future.");
            RuleFor(i => i.CustomerId)
                .MustAsync(async (customerId, cancellation) => 
                {
                    var customer = await context.Customers.FindAsync(new object[] { customerId }, cancellation);
                    return customer != null;
                })
                .WithMessage("Customer must exist in the database.");
            RuleFor(i => i.Date)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Invoice date cannot be in the future.");

            RuleFor(i => i.Customer)
                .NotNull().WithMessage("Customer information must be provided.");
        }
    }
}

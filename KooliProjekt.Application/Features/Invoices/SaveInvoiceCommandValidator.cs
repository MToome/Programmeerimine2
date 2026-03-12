using FluentValidation;
using KooliProjekt.Application.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Invoices
{
    public class SaveInvoiceCommandValidator : AbstractValidator<SaveInvoiceCommand>
    {
       
        public SaveInvoiceCommandValidator(ApplicationDbContext context) 
        {
            RuleFor(x => x.Customer)
                .NotEmpty().WithMessage("Customer name is required.")
                .MaximumLength(100).WithMessage("Customer name cannot exceed 100 characters.");
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(0).WithMessage("Id must be higher than 0")
                .NotNull().WithMessage("Id can not be null");
            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Date is required.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Date cannot be in the future.");
            RuleFor(x => x.DueDate)
                .NotEmpty().WithMessage("Due date is required.")
                .GreaterThan(x => x.Date).WithMessage("Due date must be after the invoice date.");
            RuleFor(x => x.CustomerId)
                .GreaterThan(0).WithMessage("Customer ID must be greater than 0.");

        }
    }
}

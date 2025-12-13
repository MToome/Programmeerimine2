using FluentValidation;
using KooliProjekt.Application.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Customers
{
    public class SaveCustomerCommandValidator : AbstractValidator<SaveCustomerCommand>
    {
        public SaveCustomerCommandValidator(ApplicationDbContext context)
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Customer name is required.")
                .MaximumLength(100).WithMessage("Customer name must not exceed 100 characters.")
                // Oma loogikaga valideerimise reegel
                // Siin võib kasutada DbContexti klassi
                .Custom((s, context) =>
                 {
                     // Command või query, mida valideerime
                     var command = context.InstanceToValidate;

                     // Oma valideerimise loogika
                     // koos vea lisamisega
                     //var failure = new ValidationFailure();
                     //failure.AttemptedValue = command.ProjectId;
                     //failure.ErrorMessage = "Cannot find project with id " + command.ProjectId;
                     //failure.PropertyName = nameof(command.ProjectId);

                     //context.AddFailure(failure);
                 });
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email is required.");
            RuleFor(c => c.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .MaximumLength(15).WithMessage("Phone number must not exceed 15 characters.");
            RuleFor(c => c.Address)
                .NotEmpty().WithMessage("Address is required.");
            RuleFor(c => c.City)
                .NotEmpty().WithMessage("City is required.");
            
        }
    }
}

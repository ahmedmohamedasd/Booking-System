using BackEnd.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Validations
{
    public class TicketValidation :AbstractValidator<Ticket>
    {
        public TicketValidation()
        {
            RuleFor(c => c.TicketName).NotEmpty().NotNull().WithMessage("Ticket Name Must not be null ");
            RuleFor(c => c.PriceForKids).NotEmpty().NotNull().GreaterThan(-1).WithMessage("Price For Kids Musn't be null Or Less Than 0");
            RuleFor(c => c.PriceForAdult).NotEmpty().NotNull().GreaterThan(-1).WithMessage("Price For Adult Musn't be null Or Less Than 0");
            RuleFor(c => c.Sorting).NotEmpty().NotNull().GreaterThan(-1).WithMessage("Sorting Must not be null Or less than 0");
        }
    }
}

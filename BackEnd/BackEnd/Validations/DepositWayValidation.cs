using BackEnd.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Validations
{
    public class DepositWayValidation:AbstractValidator<DepositWay>
    {
        public DepositWayValidation()
        {
            RuleFor(c => c.Name).NotEmpty().NotNull().WithMessage("Deposit Way Must be Unique and not be null ");
            RuleFor(c => c.Sorting).NotEmpty().NotNull().GreaterThan(-1).WithMessage("Sorting Must not be null Or less than 0");
        }
    }
}

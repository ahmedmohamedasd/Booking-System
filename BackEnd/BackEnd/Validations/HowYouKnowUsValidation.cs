using BackEnd.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Validations
{
    public class HowYouKnowUsValidation :AbstractValidator<HowYouKnowUs>
    {
        public HowYouKnowUsValidation()
        {
            RuleFor(c => c.WayName).NotEmpty().NotNull().WithMessage("Social Name Must be Unique and not be null ");
            RuleFor(c => c.Sorting).NotEmpty().NotNull().GreaterThan(-1).WithMessage("Sorting Must not be null Or less than 0");
        }

    }
}

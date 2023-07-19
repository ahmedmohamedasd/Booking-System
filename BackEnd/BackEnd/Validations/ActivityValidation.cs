using BackEnd.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Validations
{
    public class ActivityValidation :AbstractValidator<Activity>
    {
        public ActivityValidation()
        {
        RuleFor(c => c.ActivityName).NotEmpty().NotNull().WithMessage("Activity Name Must not be null ");
        RuleFor(c => c.ActivityPrice).NotEmpty().NotNull().GreaterThan(-1).WithMessage("Price For Activity Musn't be null Or Less Than 0");
        RuleFor(c => c.Sorting).NotEmpty().NotNull().GreaterThan(-1).WithMessage("Sorting Must not be null Or less than 0");

        }
    }
}

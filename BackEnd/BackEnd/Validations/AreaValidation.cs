using BackEnd.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Validations
{
    public class AreaValidation :AbstractValidator<Area>
    {
        public AreaValidation()
        {
            RuleFor(c => c.AreaName).NotEmpty().NotNull().WithMessage("Area Name Must not be null ");
            RuleFor(c => c.AreaRanking).NotEmpty().NotNull().GreaterThan(-1).WithMessage("Area Ranking Musn't be null Or Less Than 0");
            RuleFor(c => c.AreaMinimum).NotEmpty().NotNull().GreaterThan(-1).WithMessage("Area Minimum Must not be null Or less than 0");
        }
    }
}

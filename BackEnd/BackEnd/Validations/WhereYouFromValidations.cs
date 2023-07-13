using BackEnd.Data;
using BackEnd.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Validations
{
    public class WhereYouFromValidations: AbstractValidator<WhereYouFrom>
    {

        public WhereYouFromValidations()
        {
            RuleFor(c => c.PlaceName).NotEmpty().NotNull().WithMessage("Place Name Must be Unique and not be null ");
            RuleFor(c => c.Sorting).NotEmpty().NotNull().GreaterThan(-1).WithMessage("Sorting Must not be null Or less than 0");
        }
        //private bool UniqueName(string name)
        //{
        //    var model = ListOfPlaces.FirstOrDefault(c => c.PlaceName.ToLower() == name.ToLower());
        //    if(model != null)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}
    }
}

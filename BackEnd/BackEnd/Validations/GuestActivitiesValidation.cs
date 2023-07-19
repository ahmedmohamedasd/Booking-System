using BackEnd.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Validations
{
    public class GuestActivitiesValidation :AbstractValidator<GuestActivity>
    {
        public GuestActivitiesValidation()
        {
            RuleFor(c => c.ActivityId).NotEmpty().NotNull().WithMessage("Activity Id Must not be null ");
            RuleFor(c => c.ActivityPrice).NotEmpty().NotNull().GreaterThan(-1).WithMessage("Price  Must not be null Or Less Than 0");
            RuleFor(c => c.DateOfBooking).NotEmpty().NotNull().WithMessage("Date of Booking can not be null ");
            RuleFor(c => c.GuestId).NotEmpty().NotNull().GreaterThan(-1).WithMessage("Guest Must not be null");
            RuleFor(c => c.Quantity).GreaterThan(-1).WithMessage("Quantity Must not be null or less than 0");
            RuleFor(c => c.SubTotal).GreaterThan(-1).WithMessage("SubTotal Must not be null or less than 0");

        }
    }
}

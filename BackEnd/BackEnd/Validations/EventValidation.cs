using BackEnd.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Validations
{
    public class EventValidation :AbstractValidator<Event>
    {
       public EventValidation()
        {
            RuleFor(c => c.EventName).NotEmpty().NotNull().WithMessage("Event Name Must not be null  and not be null ");
            RuleFor(c => c.DateOfEvent).NotEmpty().NotNull().WithMessage("Date of Event Must not be null ");
            RuleFor(c => c.EventPrice).GreaterThan(-1).WithMessage("Event Price Must not be null Or less than 0");
        }
    }
}
